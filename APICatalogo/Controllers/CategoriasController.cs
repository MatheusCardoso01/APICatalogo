using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        var categorias = _context.Categorias.ToList();

        if (categorias is null)
        {
            return NotFound("404: Categorias não foram encontradas");
        }

        return categorias;
    }

    [HttpGet("{id:int}")]
    public ActionResult<Categoria> Get(int id)
    {
        var categoria = _context.Categorias.FirstOrDefault(c => c.CategoriaId == id);

        if (categoria is null)
            return NotFound("404: Categoria não encontrada");

        return categoria;
    }

    //[HttpGet("produtos-categorias")]
    //public ActionResult GetAll()
    //{
    //    var categorias = _context.Categorias.ToList();
    //    var produtos = _context.Produtos.ToList();

    //    var resultado = new
    //    {
    //        Categoria = categorias,
    //        Produtos = produtos
    //    };

    //    if (resultado is null)
    //        return NotFound("404: Nenenhum resultado e nenhuma categoria encontrados");

    //    return Ok(resultado);
    //}


}
