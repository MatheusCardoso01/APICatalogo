using APICatalogo.Context;
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
    private readonly AppDbContext _context;

    public CategoriasController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Categoria>>> GetAsync()
    {
        try
        {
            //AsNoTracking somente para Get
            var categorias = await _context.Categorias.AsNoTracking().ToListAsync();

            if (categorias is null)
                return NotFound("404: Categorias não encontradas");

            return categorias;
        }
        catch (Exception)
        {
            //Uso da classe StatusCodes para tratamento
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro inesperado");
        }
    }

    [HttpGet("{id:int}", Name = "ObterCategoria")]
    public async Task<ActionResult<Categoria>> GetAsync(int id)
    {
        try
        {
            var categoria = await _context.Categorias.FirstOrDefaultAsync(c => c.CategoriaId == id);

            if (categoria is null)
                return NotFound("404: Categoria não encontrada");

            return categoria;
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro inesperado");
        }
    }

    [HttpGet("categorias_produtos")]
    public ActionResult<IEnumerable<Categoria>> GetCategoriasEProdutos()
    {
        try
        {
            //O EntityFramework usa a Biblioteca LINQ para fazer consultas avançadas
            return _context.Categorias.Include(p => p.Produtos).Where(c => c.CategoriaId <= 5).Take(100).ToList();
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro inesperado");
        }
    }


    [HttpPost]
    public IActionResult Post(Categoria categoria)
    {
        try
        {
            if (categoria is null)
                return BadRequest("Dados inválidos");

            _context.Categorias.Add(categoria);
            _context.SaveChanges();

            return new CreatedAtRouteResult("ObterCategoria",
                new { id = categoria.CategoriaId }, categoria);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro inesperado");
        }
    }

    [HttpPut("{id:int}")]
    public IActionResult Put(int id, Categoria categoria)
    {
        try
        {
            if (id != categoria.CategoriaId)
                return BadRequest("Dados inválidos");

            _context.Categorias.Update(categoria);
            _context.SaveChanges();

            return Ok(categoria);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro inesperado");
        }
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        try
        {
            Categoria categoria = _context.Categorias.FirstOrDefault(c => c.CategoriaId == id);

            if (categoria is null)
                return NotFound("404: Categoria não encontrada");

            _context.Categorias.Remove(categoria);
            _context.SaveChanges();

            return Ok(categoria);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro inesperado");
        }
    }

    // Ex. Serviço MeuServico (IMeuServico poderia ser atributo do controller, mas é só exemplo)
    [HttpGet("UsandoFromServices:MeuServico/{saudacao}")]
    public ActionResult<string> GetSaudacaoFromServices([FromServices] IMeuServico meuServico, string saudacao) // [FromServices] é desnecessário em versões recentes do .NET
    {
        try
        {
            return meuServico.Saudacao(saudacao);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro inesperado");
        }
    }
}
