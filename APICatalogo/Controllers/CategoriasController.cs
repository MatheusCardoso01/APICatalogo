using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers;

[Route("[controller]")]
[ApiController]
public class CategoriasController : ControllerBase
{
    private readonly AppDbContext _context;

    public CategoriasController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Categoria>> Get()
    {
        try
        {
            //AsNoTracking somente para Get
            var categorias = _context.Categorias.AsNoTracking().ToList();

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
    public ActionResult<Categoria> Get(int id)
    {
        try
        {
            var categoria = _context.Categorias.FirstOrDefault(c => c.CategoriaId == id);

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
    public ActionResult Post(Categoria categoria)
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
    public ActionResult Put(int id, Categoria categoria)
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
    public ActionResult Delete(int id)
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
}
