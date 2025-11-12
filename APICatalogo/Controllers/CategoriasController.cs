using APICatalogo.Context;
using APICatalogo.Filters;
using APICatalogo.Models;
using APICatalogo.Repositories;
using APICatalogo.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers;

[Route("api/[controller]")]
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


    [HttpGet]
    [ServiceFilter(typeof(ApiLoggingFilter))]
    public async Task<ActionResult<IEnumerable<Categoria>>> GetAllAsync()
    {

        var categorias = await _repository.GetCategoriasAsync();

        if (!categorias.Any()) 
        {
            _logger.LogInformation($"Não Encontrado");

            return NotFound("Não Encontrado");
        }

        return Ok(categorias);
    }

    [HttpGet("{id:int}", Name = "ObterCategoria")]
    public async Task<ActionResult<Categoria>> GetAsync(int id)
    {

        var categoria = await _repository.GetCategoriaAsync(id);


        if (categoria is null)
        {
            _logger.LogInformation($"Não Encontrado");

            return NotFound("$Não Encontrado");
        }

        return Ok(categoria);
    }

    [HttpGet("categorias_produtos")]
    public async Task<ActionResult<IEnumerable<Categoria>>> GetCategoriasEProdutosAsync()
    {
        //O EntityFramework usa a Biblioteca LINQ para fazer consultas avançadas
        IEnumerable<Categoria> categoriasEProdutos = await _repository.GetCategoriasEProdutosAsync();

        if (!categoriasEProdutos.Any()) 
        {
            _logger.LogInformation($"Não Encontrado");

            return NotFound("Não Encontrado");
        }

        return Ok(categoriasEProdutos);
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync(Categoria categoria)
    {

        if (categoria is null) 
        {
            _logger.LogInformation($"Dados Inválidos");

            return BadRequest("Dados inválidos");
        }

        var categoriaCriada = await _repository.CreateCategoriaAsync(categoria);
        
        return new CreatedAtRouteResult("ObterCategoria",
            new { id = categoriaCriada.CategoriaId }, categoriaCriada);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> PutAsync(int id, Categoria categoria)
    {

        if (id != categoria.CategoriaId) 
        {
            _logger.LogInformation($"Dados Inválidos");

            return BadRequest("Dados inválidos");
        }

        await _repository.UpdateCategoriaAsync(categoria);        

        return Ok(categoria);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {

        Categoria categoria = await _repository.DeleteCategoriaAsync(id);

        if (categoria is null) 
        {
            _logger.LogInformation($"Não Encontrado");

            return NotFound("Não Encontrado");
        }

        return Ok(categoria);
    }

    // Ex. Serviço MeuServico (IMeuServico poderia ser atributo do controller, mas é só exemplo)
    [HttpGet("UsandoFromServices:MeuServico/{saudacao}")]
    public ActionResult<string> GetSaudacaoFromServices([FromServices] IMeuServico meuServico, string saudacao) // [FromServices] é desnecessário em versões recentes do .NET
    {
        return meuServico.Saudacao(saudacao);
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
