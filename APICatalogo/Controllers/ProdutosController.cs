using APICatalogo.Context;
using APICatalogo.Models;
using APICatalogo.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProdutosController : ControllerBase
{
    private readonly IUnityOfWork _uof;
    private readonly ILogger _logger;

    public ProdutosController(IUnityOfWork uof, ILogger<ProdutosController> logger)
    {
        _uof = uof;
        _logger = logger;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Produto>> GetAll()
    {
        var produtos = _uof.ProdutoRepository.GetAll();

        if (produtos is null)
        {
            return NotFound($"Não Encontrado");
        }

        return Ok(produtos);
    }

    // "/primeiro" em vez de "primeiro" ignora o Route
    [HttpGet("primeiro")]
    [HttpGet("/primeiro")]
    public ActionResult<Produto> GetPrimeiro()
    {
        var primeiroProduto = _uof.ProdutoRepository.GetPrimeiro();

        if (primeiroProduto is null)
        {
            return NotFound($"Não Encontrado");
        }

        return Ok(primeiroProduto);

    }

    // [FromRoute] é um Atributo de Model Binding
    [HttpGet("{id:int:min(1)}", Name = "ObterProduto")]
    public ActionResult<Produto> Get([FromRoute] int id) // [FromRoute] desnecessário
    {

        var produto = _uof.ProdutoRepository.Get(p => p.ProdutoId == id);

        if (produto is null)
        {
            return NotFound($"Não Encontrado");
        }

        return Ok(produto);

    }

    [HttpGet("porcategoria/{id:int}")]
    public ActionResult<IEnumerable<Produto>> GetProdutosPorCategoriaEspecifica(int id)
    {
        var produtos = _uof.ProdutoRepository.GetProdutosPorCategoriaEspecifica(id);

        if (produtos is null) 
        {
            return NotFound($"Não Encontrado");
        }

        return Ok(produtos);
    }

    [HttpPost]
    public IActionResult Post(Produto produto)
    {

        if (produto is null)
            return BadRequest("Dados inválidos");

        var produtoNovo = _uof.ProdutoRepository.Create(produto);
        _uof.Commit();

        return new CreatedAtRouteResult("ObterProduto",
            new { id = produtoNovo.ProdutoId }, produtoNovo);
    }

    [HttpPut("{id:int}")]
    public IActionResult Put(int id, Produto produto)
    {

        if (id != produto.ProdutoId)
        {
            return BadRequest("Dados inválidos");
        }

        _uof.ProdutoRepository.Update(produto);
        _uof.Commit();

        return Ok(produto);

    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var produto = _uof.ProdutoRepository.Get(p => p.ProdutoId == id);

        if (produto is null)
        {
            return NotFound($"Não Encontrado");
        }

        var produtoDeletado = _uof.ProdutoRepository.Delete(produto);
        _uof.Commit();

        return Ok(produtoDeletado);
    }
}