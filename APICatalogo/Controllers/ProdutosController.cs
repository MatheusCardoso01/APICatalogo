using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProdutosController : ControllerBase
{
    private readonly AppDbContext _context;

    public ProdutosController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Produto>> Get()
    {
        try
        {
            //Optei por nao usar AsNoTracking() aqui de exemplo
            //Take pra evitar buscar milhões de valores
            var produtos = _context.Produtos.Take(10).ToList();

            if (produtos is null)
            {
                return NotFound("404: Produtos não encontrados");
            }

            return produtos;
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro inesperado");
        }
    }

    // "/primeiro" em vez de "primeiro" ignora o Route
    [HttpGet("primeiro")]
    [HttpGet("/primeiro")]
    public ActionResult<Produto> GetPrimeiro()
    {
        try
        {
            //Optei por nao usar AsNoTracking() aqui de exemplo
            //Take pra evitar buscar milhões de valores
            var primeiroProduto = _context.Produtos.FirstOrDefault();

            if (primeiroProduto is null)
            {
                return NotFound("404: Produtos não encontrados");
            }

            return primeiroProduto;
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro inesperado");
        }
    }

    [HttpGet("{id:int:min(1)}", Name = "ObterProduto")]
    public ActionResult<Produto> Get(int id)
    {
        try
        {
            var produto = _context.Produtos.FirstOrDefault(p => p.ProdutoId == id);

            if (produto is null)
            {
                return NotFound("404: Produto não encontrado");
            }

            return produto;
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro inesperado");
        }
    }

    [HttpPost]
    public IActionResult Post(Produto produto)
    {
        try
        {
            if (produto is null)
                return BadRequest("Dados inválidos");

            _context.Produtos.Add(produto);
            _context.SaveChanges();

            return new CreatedAtRouteResult("ObterProduto",
                new { id = produto.ProdutoId }, produto);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro inesperado");
        }
    }

    [HttpPut("{id:int}")]
    public IActionResult Put(int id, Produto produto)
    {
        try
        {
            if (id != produto.ProdutoId)
            {
                return BadRequest("Dados inválidos");
            }

            _context.Entry(produto).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(produto);
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
            var produto = _context.Produtos.FirstOrDefault(p => p.ProdutoId == id);

            if (produto is null)
            {
                return NotFound("404: Produto não encontrado");
            }

            _context.Produtos.Remove(produto);
            _context.SaveChanges();

            return Ok(produto);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro inesperado");
        }
    }
}

