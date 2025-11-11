using APICatalogo.Context;
using APICatalogo.Models;
using APICatalogo.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProdutosController : ControllerBase
{
    private readonly IProdutoRepository _repository;
    private readonly ILogger _logger;

    public ProdutosController(IProdutoRepository repository, ILogger<ProdutosController> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Produto>> Get()
    {
        var produtos = _repository.GetProdutos();

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
        var primeiroProduto = _repository.GetPrimeiro();

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

        var produto = _repository.GetProduto(id);

        if (produto is null)
        {
            return NotFound($"Não Encontrado");
        }

        return Ok(produto);

    }

    [HttpPost]
    public IActionResult Post(Produto produto)
    {

        if (produto is null)
            return BadRequest("Dados inválidos");

        var produtoModificado = _repository.CreateProduto(produto);

        return new CreatedAtRouteResult("ObterProduto",
            new { id = produtoModificado.ProdutoId }, produtoModificado);
    }

    [HttpPut("{id:int}")]
    public IActionResult Put(int id, Produto produto)
    {

        if (id != produto.ProdutoId)
        {
            return BadRequest("Dados inválidos");
        }

        _repository.UpdateProduto(produto);

        return Ok(produto);

    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {

        var produto = _repository.DeleteProduto(id);

        if (produto is null)
        {
            return NotFound($"Não Encontrado");
        }

        return Ok(produto);
    }
}

