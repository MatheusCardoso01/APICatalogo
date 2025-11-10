using APICatalogo.Context;
using APICatalogo.Filters;
using APICatalogo.Models;
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
    private readonly AppDbContext _context; // acessar o banco de dados
    private readonly ILogger _logger;
    //private readonly IConfiguration _configuration; // acessar o appsettings.json

    // contrutor
    public CategoriasController(AppDbContext context, ILogger<CategoriasController> logger)
    {
        _context = context;
        _logger = logger;
        //_configuration = configuration;
    }

    //[HttpGet("LerArquivoConfiguracao")] // lê informações do appsettings.json
    //public string GetValores()
    //{ 
    //    var valor1 = _configuration["chave1"];
    //    var valor2 = _configuration["chave2"];

    //    var secao1 = _configuration["secao1:chave2"];

    //    return $"chave1 = {valor1} \nchave2 = {valor2} \nsecao1 = {secao1}";
    //}

    [HttpGet]
    [ServiceFilter(typeof(ApiLoggingFilter))]
    public async Task<ActionResult<IEnumerable<Categoria>>> GetAsync()
    {

        var categorias = await _context.Categorias.AsNoTracking().ToListAsync();

        if (categorias is null)
            return NotFound("404: Categorias não encontradas");

        return categorias;
    }

    [HttpGet("{id:int}", Name = "ObterCategoria")]
    public async Task<ActionResult<Categoria>> GetAsync(int id)
    {

        var categoria = await _context.Categorias.FirstOrDefaultAsync(c => c.CategoriaId == id);


        if (categoria is null)
        {
            _logger.LogInformation($"LOG: ================== GET api/Categorias/id = {id} NOT FOUND ==================");

            return NotFound("404: Categoria não encontrada");
        }

        return categoria;
    }

    [HttpGet("categorias_produtos")]
    public ActionResult<IEnumerable<Categoria>> GetCategoriasEProdutos()
    {
        //O EntityFramework usa a Biblioteca LINQ para fazer consultas avançadas
        return _context.Categorias.Include(p => p.Produtos).Where(c => c.CategoriaId <= 5).Take(100).ToList();
    }


    [HttpPost]
    public IActionResult Post(Categoria categoria)
    {

        if (categoria is null)
            return BadRequest("Dados inválidos");

        _context.Categorias.Add(categoria);
        _context.SaveChanges();

        return new CreatedAtRouteResult("ObterCategoria",
            new { id = categoria.CategoriaId }, categoria);
    }

    [HttpPut("{id:int}")]
    public IActionResult Put(int id, Categoria categoria)
    {

        if (id != categoria.CategoriaId)
            return BadRequest("Dados inválidos");

        _context.Categorias.Update(categoria);
        _context.SaveChanges();

        return Ok(categoria);
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {

        Categoria categoria = _context.Categorias.FirstOrDefault(c => c.CategoriaId == id);

        if (categoria is null)
            return NotFound("404: Categoria não encontrada");

        _context.Categorias.Remove(categoria);
        _context.SaveChanges();

        return Ok(categoria);
    }

    // Ex. Serviço MeuServico (IMeuServico poderia ser atributo do controller, mas é só exemplo)
    [HttpGet("UsandoFromServices:MeuServico/{saudacao}")]
    public ActionResult<string> GetSaudacaoFromServices([FromServices] IMeuServico meuServico, string saudacao) // [FromServices] é desnecessário em versões recentes do .NET
    {
        return meuServico.Saudacao(saudacao);
    }

}
