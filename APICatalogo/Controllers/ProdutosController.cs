using APICatalogo.Context;
using APICatalogo.DTOs;
using APICatalogo.Models;
using APICatalogo.Repositories.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

    [HttpGet]
    public ActionResult<IEnumerable<ProdutoDTO>> GetAll()
    {
        var produtos = _uof.ProdutoRepository.GetAll();

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
    public ActionResult<ProdutoDTO> GetPrimeiro()
    {
        var primeiroProduto = _uof.ProdutoRepository.GetPrimeiro();

        if (primeiroProduto is null)
        {
            return NotFound($"Não Encontrado");
        }

        var primeiroProdutoDTO = _mapper.Map<ProdutoDTO>(primeiroProduto);

        return Ok(primeiroProdutoDTO);

    }

    // [FromRoute] é um Atributo de Model Binding
    [HttpGet("{id:int:min(1)}", Name = "ObterProduto")]
    public ActionResult<ProdutoDTO> Get([FromRoute] int id) // [FromRoute] desnecessário
    {

        var produto = _uof.ProdutoRepository.Get(p => p.ProdutoId == id);

        if (produto is null)
        {
            return NotFound($"Não Encontrado");
        }

        var produtoDTO = _mapper.Map<ProdutoDTO>(produto);

        return Ok(produtoDTO);

    }

    [HttpGet("porcategoria/{id:int}")]
    public ActionResult<IEnumerable<ProdutoDTO>> GetProdutosPorCategoriaEspecifica(int id)
    {
        var produtos = _uof.ProdutoRepository.GetProdutosPorCategoriaEspecifica(id);

        if (produtos is null)
        {
            return NotFound($"Não Encontrado");
        }
        // var destino = _mapper.Map<Destino>(origem);
        var produtosDTO = _mapper.Map<IEnumerable<ProdutoDTO>>(produtos);

        return Ok(produtosDTO);
    }

    [HttpPost]
    public ActionResult<ProdutoDTO> Post(ProdutoDTO produtoDTO)
    {

        if (produtoDTO is null)
            return BadRequest("Dados inválidos");

        var produto = _mapper.Map<Produto>(produtoDTO);

        var produtoNovo = _uof.ProdutoRepository.Create(produto);
        _uof.Commit();

        var produtoNovoDTO = _mapper.Map<ProdutoDTO>(produtoNovo);

        return new CreatedAtRouteResult("ObterProduto",
            new { id = produtoNovoDTO.ProdutoId }, produtoNovoDTO);
    }

    [HttpPatch("{id:int}/UpdatePartial")]
    public ActionResult<ProdutoDTOUpdateResponse> Patch(int id, JsonPatchDocument<ProdutoDTOUpdateRequest> patchProdutoDTO)
    {
        if (patchProdutoDTO is null || id <= 0)
        {
            return BadRequest("Dados inválidos");
        }

        var produto = _uof.ProdutoRepository.Get(p => p.ProdutoId == id);

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
        _uof.Commit();

        return Ok(_mapper.Map<ProdutoDTOUpdateResponse>(produto));
    }

    [HttpPut("{id:int}")]
    public ActionResult<ProdutoDTO> Put(int id, ProdutoDTO produtoDTO)
    {

        if (id != produtoDTO.ProdutoId)
        {
            return BadRequest("Dados inválidos");
        }

        var produto = _mapper.Map<Produto>(produtoDTO);

        var produtoAtualizado = _uof.ProdutoRepository.Update(produto);
        _uof.Commit();

        var produtoAtualizadoDTO = _mapper.Map<ProdutoDTO>(produtoAtualizado);

        return Ok(produtoAtualizadoDTO);
    }

    [HttpDelete("{id:int}")]
    public ActionResult<ProdutoDTO> Delete(int id)
    {
        var produto = _uof.ProdutoRepository.Get(p => p.ProdutoId == id);

        if (produto is null)
        {
            return NotFound($"Não Encontrado");
        }

        var produtoDeletado = _uof.ProdutoRepository.Delete(produto);
        _uof.Commit();

        var produtoDeletadoDTO = _mapper.Map<ProdutoDTO>(produtoDeletado);

        return Ok(produtoDeletadoDTO);
    }
}