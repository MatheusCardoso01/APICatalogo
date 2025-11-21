using APICatalogo.Context;
using APICatalogo.DTOs;
using APICatalogo.Models;
using APICatalogo.Pagination;
using APICatalogo.Repositories.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;

namespace APICatalogo.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProdutosController : ControllerBase
{
    private readonly IUnityOfWork _uof;
    private readonly ILogger _logger;
    private readonly IMapper _mapper;

    public ProdutosController(IUnityOfWork uof, ILogger<ProdutosController> logger, IMapper mapper)
    {
        _uof = uof;
        _logger = logger;
        _mapper = mapper;
    }

    // endpoints

    [HttpGet]
    [Authorize(Policy = "UserOnly")]
    public async Task<ActionResult<IEnumerable<ProdutoDTO>>> GetAllAsync()
    {
        var produtos = await _uof.ProdutoRepository.GetAllAsync();

        if (produtos is null)
        {
            return NotFound($"Não Encontrado");
        }

        var produtosDTO = _mapper.Map<IEnumerable<ProdutoDTO>>(produtos);

        return Ok(produtosDTO);
    }

    // "/primeiro" em vez de "primeiro" ignora o Route
    [HttpGet("primeiro")]
    [HttpGet("/primeiro")]
    public async Task<ActionResult<ProdutoDTO>> GetPrimeiroAsync()
    {
        var primeiroProduto = await _uof.ProdutoRepository.GetPrimeiroAsync();

        if (primeiroProduto is null)
        {
            return NotFound($"Não Encontrado");
        }

        var primeiroProdutoDTO = _mapper.Map<ProdutoDTO>(primeiroProduto);

        return Ok(primeiroProdutoDTO);

    }

    // [FromRoute] é um Atributo de Model Binding
    [HttpGet("{id:int:min(1)}", Name = "ObterProduto")]
    public async Task<ActionResult<ProdutoDTO>> GetAsync([FromRoute] int id) // [FromRoute] desnecessário
    {

        var produto = await _uof.ProdutoRepository.GetAsync(p => p.ProdutoId == id);

        if (produto is null)
        {
            return NotFound($"Não Encontrado");
        }

        var produtoDTO = _mapper.Map<ProdutoDTO>(produto);

        return Ok(produtoDTO);

    }

    [HttpGet("porcategoria/{id:int}")]
    public async Task<ActionResult<IEnumerable<ProdutoDTO>>> GetProdutosPorCategoriaEspecificaAsync(int id)
    {
        var produtos = await _uof.ProdutoRepository.GetProdutosPorCategoriaEspecificaAsync(id);

        if (produtos is null)
        {
            return NotFound($"Não Encontrado");
        }
        // var destino = _mapper.Map<Destino>(origem);
        var produtosDTO = _mapper.Map<IEnumerable<ProdutoDTO>>(produtos);

        return Ok(produtosDTO);
    }

    [HttpPost]
    public async Task<ActionResult<ProdutoDTO>> PostAsync(ProdutoDTO produtoDTO)
    {

        if (produtoDTO is null)
            return BadRequest("Dados inválidos");

        var produto = _mapper.Map<Produto>(produtoDTO);

        var produtoNovo = _uof.ProdutoRepository.Create(produto);
        await _uof.CommitAsync();

        var produtoNovoDTO = _mapper.Map<ProdutoDTO>(produtoNovo);

        return new CreatedAtRouteResult("ObterProduto",
            new { id = produtoNovoDTO.ProdutoId }, produtoNovoDTO);
    }

    [HttpPatch("{id:int}/UpdatePartial")]
    public async Task<ActionResult<ProdutoDTOUpdateResponse>> PatchAsync(int id, JsonPatchDocument<ProdutoDTOUpdateRequest> patchProdutoDTO)
    {
        if (patchProdutoDTO is null || id <= 0)
        {
            return BadRequest("Dados inválidos");
        }

        var produto = await _uof.ProdutoRepository.GetAsync(p => p.ProdutoId == id);

        if (produto is null)
        {
            return NotFound($"Não Encontrado");
        }

        var produtoUpdateRequest = _mapper.Map<ProdutoDTOUpdateRequest>(produto);

        patchProdutoDTO.ApplyTo(produtoUpdateRequest, ModelState); // ModelState verifica se houve erros na aplicação do patch

        // verifica se o PATCH incluiu DataCadastro
        bool dataCadastroAlterada = patchProdutoDTO.Operations
            .Any(op => op.path.Equals("/DataCadastro", StringComparison.OrdinalIgnoreCase));

        // se não incluiu, atribui a data atual da alteração
        if (!dataCadastroAlterada)
        {
            produtoUpdateRequest.DataCadastro = DateTime.Now;
        }

        if (!ModelState.IsValid || !TryValidateModel(produtoUpdateRequest))
        {
            return BadRequest(ModelState);
        }

        _mapper.Map(produtoUpdateRequest, produto);

        _uof.ProdutoRepository.Update(produto);

        await _uof.CommitAsync();

        return Ok(_mapper.Map<ProdutoDTOUpdateResponse>(produto));
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<ProdutoDTO>> PutAsync(int id, ProdutoDTO produtoDTO)
    {

        if (id != produtoDTO.ProdutoId)
        {
            return BadRequest("Dados inválidos");
        }

        var produto = _mapper.Map<Produto>(produtoDTO);

        var produtoAtualizado = _uof.ProdutoRepository.Update(produto);

        await _uof.CommitAsync();

        var produtoAtualizadoDTO = _mapper.Map<ProdutoDTO>(produtoAtualizado);

        return Ok(produtoAtualizadoDTO);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<ProdutoDTO>> DeleteAsync(int id)
    {
        var produto = await _uof.ProdutoRepository.GetAsync(p => p.ProdutoId == id);

        if (produto is null)
        {
            return NotFound($"Não Encontrado");
        }

        var produtoDeletado = _uof.ProdutoRepository.Delete(produto);

        await _uof.CommitAsync();

        var produtoDeletadoDTO = _mapper.Map<ProdutoDTO>(produtoDeletado);

        return Ok(produtoDeletadoDTO);
    }

    [HttpGet("pagination")]
    public async Task<ActionResult<IEnumerable<ProdutoDTO>>> GetAsync([FromQuery] Parameters produtosParams)
    {
        var produtos = await _uof.ProdutoRepository.GetProdutosAsync(produtosParams);

        return ObterProdutosPaginados(produtos);
    }

    [HttpGet("filter/preco/pagination")]
    public async Task<ActionResult<IEnumerable<ProdutoDTO>>> GetProdutosFilterPrecoAsync([FromQuery] ProdutosFiltroPreco produtosFilterParams)
    {        
        var produtos = await _uof.ProdutoRepository.GetProdutosFiltroPrecoAsync(produtosFilterParams);

        return ObterProdutosPaginados(produtos);
    }

    // métodos de endpoint

    private ActionResult<IEnumerable<ProdutoDTO>> ObterProdutosPaginados(PagedList<Produto>? produtos)
    {
        if (produtos is null) return NotFound($"Não Encontrado");

        var metadata = new
        {
            produtos.TotalCount,
            produtos.PageSize,
            produtos.CurrentPage,
            produtos.TotalPages,
            produtos.HasNext,
            produtos.HasPrevious
        };

        Response.Headers.Append("X-Pagination", JsonConvert.SerializeObject(metadata));

        var produtosDTO = _mapper.Map<IEnumerable<ProdutoDTO>>(produtos);

        return Ok(produtosDTO);
    }

}