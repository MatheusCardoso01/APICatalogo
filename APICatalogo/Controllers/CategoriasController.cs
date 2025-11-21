using APICatalogo.Context;
using APICatalogo.DTOs;
using APICatalogo.DTOs.Mappings;
using APICatalogo.Filters;
using APICatalogo.Models;
using APICatalogo.Pagination;
using APICatalogo.Repositories.Interfaces;
using APICatalogo.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace APICatalogo.Controllers;

[Route("api/[controller]")]
[EnableCors("OrigensComAcessoPermitido")]
// [EnableRateLimiting("fixedwindow")] usa a Global se não tiver essa anotação
[ApiController]
public class CategoriasController : ControllerBase
{
    // atributos
    private readonly ICategoriaRepository _repository; // acessar o repository
    private readonly ILogger _logger;
    //private readonly IConfiguration _configuration; // acessar o appsettings.json

    // construtor
    public CategoriasController(ICategoriaRepository repository, ILogger<CategoriasController> logger)
    {
        _repository = repository;
        _logger = logger;
        //_configuration = configuration;
    }

    // endpoints

    [HttpGet]
    [ServiceFilter(typeof(ApiLoggingFilter))]
    public async Task<ActionResult<IEnumerable<CategoriaDTO>>> GetAllAsync()
    {

        var categorias = await _repository.GetCategoriasAsync();

        if (!categorias.Any()) 
        {
            _logger.LogInformation($"Não Encontrado");

            return NotFound("Não Encontrado");
        }

        var categoriasDTO = categorias.ToCategoriaDTOList();

        return Ok(categoriasDTO);
    }

    [DisableCors]
    [DisableRateLimiting] // esse e o de cima só para exemplificar
    [HttpGet("{id:int}", Name = "ObterCategoria")]
    public async Task<ActionResult<CategoriaDTO>> GetAsync(int id)
    {

        var categoria = await _repository.GetCategoriaAsync(id);

        if (categoria is null)
        {
            _logger.LogInformation($"Não Encontrado");

            return NotFound("$Não Encontrado");
        }

        var categoriaDTO = categoria.ToCategoriaDTO();

        return Ok(categoriaDTO);
    }

    [HttpGet("categorias_produtos")]
    public async Task<ActionResult<IEnumerable<CategoriaDTO>>> GetCategoriasEProdutosAsync()
    {
        //O EntityFramework usa a Biblioteca LINQ para fazer consultas avançadas
        IEnumerable<Categoria> categoriasEProdutos = await _repository.GetCategoriasEProdutosAsync();

        if (!categoriasEProdutos.Any()) 
        {
            _logger.LogInformation($"Não Encontrado");

            return NotFound("Não Encontrado");
        }

        var categoriasEProdutosDTO = categoriasEProdutos.ToCategoriaDTOList();

        return Ok(categoriasEProdutosDTO);
    }

    [HttpPost]
    public async Task<ActionResult<CategoriaDTO>> PostAsync(CategoriaDTO categoriaDTO)
    {

        if (categoriaDTO is null) 
        {
            _logger.LogInformation($"Dados Inválidos");

            return BadRequest("Dados inválidos");
        }

        var categoria = categoriaDTO.ToCategoria();

        var categoriaCriada = await _repository.CreateCategoriaAsync(categoria);

        var novaCategoriaDTO = categoriaCriada.ToCategoriaDTO();

        return new CreatedAtRouteResult("ObterCategoria",
            new { id = novaCategoriaDTO.CategoriaId }, novaCategoriaDTO);
    }

    [HttpPatch("{id:int}/UpdatePartial")]
    public async Task<ActionResult<CategoriaDTOUpdateResponse>> Patch(int id, JsonPatchDocument<CategoriaDTOUpdateRequest> patchCategoriaDTO)
    {
        if (patchCategoriaDTO is null) 
        {
            return BadRequest("Dados inválidos");
        }

        var categoria = await _repository.GetCategoriaAsync(id);

        if (categoria is null)
        {
            return NotFound($"Não Encontrado");
        }

        var categoriaUpdateRequest = categoria.ToCategoriaUpdateRequest();

        patchCategoriaDTO.ApplyTo(categoriaUpdateRequest, ModelState);

        if (!ModelState.IsValid || !TryValidateModel(categoriaUpdateRequest))
        { 
            return BadRequest(ModelState);
        }

        categoria = categoria.UpdateCategoriaFromCategoriaUpdateRequest(categoriaUpdateRequest);

        categoria = await _repository.UpdateCategoriaAsync(categoria);

        var categoriaAtualizadaUpdateResponse = categoria.ToCategoriaUpdateResponse();

        return Ok(categoriaAtualizadaUpdateResponse);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<CategoriaDTO>> PutAsync(int id, CategoriaDTO categoriaDTO)
    {

        if (id != categoriaDTO.CategoriaId) 
        {
            _logger.LogInformation($"Dados Inválidos");

            return BadRequest("Dados inválidos");
        }

        var categoria = categoriaDTO.ToCategoria();

        var categoriaCriada = await _repository.UpdateCategoriaAsync(categoria);

        var categoriaAtualizadaDTO = categoriaCriada.ToCategoriaDTO();

        return Ok(categoriaAtualizadaDTO);
    }

    [HttpDelete("{id:int}")]
    [Authorize(Policy = "AdminOnlu")]
    public async Task<ActionResult<CategoriaDTO>> DeleteAsync(int id)
    {

        Categoria categoria = await _repository.DeleteCategoriaAsync(id);

        if (categoria is null) 
        {
            _logger.LogInformation($"Não Encontrado");

            return NotFound("Não Encontrado");
        }

        var categoriaDeletadaDTO = categoria.ToCategoriaDTO();

        return Ok(categoriaDeletadaDTO);
    }

    // Ex. Serviço MeuServico (IMeuServico poderia ser atributo do controller, mas é só exemplo)
    [HttpGet("UsandoFromServices:MeuServico/{saudacao}")]
    public ActionResult<string> GetSaudacaoFromServices([FromServices] IMeuServico meuServico, string saudacao) // [FromServices] é desnecessário em versões recentes do .NET
    {
        return meuServico.Saudacao(saudacao);
    }

    [HttpGet("pagination")]
    public async Task<ActionResult<IEnumerable<CategoriaDTO>>> Get([FromQuery] Parameters categoriasParams)
    {
        var categorias = await _repository.GetCategoriasAsync(categoriasParams);

        return ObterCategoriasPaginadas(categorias);
    }


    [HttpGet("filter/nome/pagination")]
    public async Task<ActionResult<IEnumerable<CategoriaDTO>>> GetCategoriaFilterNome([FromQuery] CategoriaFiltroNome categoriaFiltroParams)
    { 
        var categorias = await _repository.GetCategoriaFiltroNomeAsync(categoriaFiltroParams);

        return ObterCategoriasPaginadas(categorias);
    }

    // métodos do endpoint

    private ActionResult<IEnumerable<CategoriaDTO>> ObterCategoriasPaginadas(PagedList<Categoria>? categorias)
    {
        if (categorias is null) return NotFound($"Não Encontrado");

        var metadata = new
        {
            categorias.TotalCount,
            categorias.PageSize,
            categorias.CurrentPage,
            categorias.TotalPages,
            categorias.HasNext,
            categorias.HasPrevious
        };

        Response.Headers.Append("X-Pagination", JsonConvert.SerializeObject(metadata));

        var categoriasDTO = categorias.ToCategoriaDTOList();

        return Ok(categoriasDTO);
    }

    //[HttpGet("LerArquivoConfiguracao")] // lê informações do appsettings.json
    //public string GetValores()
    //{ 
    //    var valor1 = _configuration["chave1"];
    //    var valor2 = _configuration["chave2"];

    //    var secao1 = _configuration["secao1:chave2"];

    //    return $"chave1 = {valor1} \nchave2 = {valor2} \nsecao1 = {secao1}";
    //}
}