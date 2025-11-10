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
    public async Task<ActionResult<IEnumerable<Produto>>> GetAsync()
    {
        try // o uso de middleware é melhor para tratamento de erros, ver Extensions/ApiExceptionMiddlewareExtensions.cs
        {
            //Optei por nao usar AsNoTracking() aqui de exemplo
            //Take pra evitar buscar milhões de valores
            var produtos = await _context.Produtos.Take(10).ToListAsync();

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
        //throw new DataMisalignedException("Erro de Exemplo - Tratamento via Middleware");
        string[] teste = null;
        if (teste.Length > 0)
        {
        }

        //Optei por nao usar AsNoTracking() aqui de exemplo
        //Take pra evitar buscar milhões de valores
        var primeiroProduto = _context.Produtos.FirstOrDefault();

        if (primeiroProduto is null)
        {
            return NotFound("404: Produtos não encontrados");
        }

        return primeiroProduto;

    }

    // [FromRoute] é um Atributo de Model Binding
    [HttpGet("{id:int:min(1)}", Name = "ObterProduto")]
    public async Task<ActionResult<Produto>> GetAsync([FromRoute] int id) // [FromRoute] desnecessário
    {

        var produto = await _context.Produtos.FirstOrDefaultAsync(p => p.ProdutoId == id);

        if (produto is null)
        {
            return NotFound("404: Produto não encontrado");
        }

        return produto;

    }

    [HttpPost]
    public IActionResult Post(Produto produto)
    {

        if (produto is null)
            return BadRequest("Dados inválidos");

        _context.Produtos.Add(produto);
        _context.SaveChanges();

        return new CreatedAtRouteResult("ObterProduto",
            new { id = produto.ProdutoId }, produto);

    }

    [HttpPut("{id:int}")]
    public IActionResult Put(int id, Produto produto)
    {

        if (id != produto.ProdutoId)
        {
            return BadRequest("Dados inválidos");
        }

        _context.Entry(produto).State = EntityState.Modified;
        _context.SaveChanges();

        return Ok(produto);

    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
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
}

