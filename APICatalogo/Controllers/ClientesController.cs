using APICatalogo.Models;
using APICatalogo.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APICatalogo.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClientesController : ControllerBase
{
    private readonly IClienteRepository _repository;

    public ClientesController(IClienteRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Cliente>> GetAll()
    { 
        var clientes = _repository.GetAll();

        if (clientes is null)
        {
            return NotFound($"Não Encontrado");
        }

        return Ok(clientes);
    }

    [HttpGet("{id:int}", Name = "ObterCliente")]
    public ActionResult<Cliente> Get(int id)
    {
        var cliente = _repository.Get(c => c.ClienteId == id);

        if (cliente is null)
        {
            return NotFound($"Não Encontrado");
        }

        return Ok(cliente);
    }

    [HttpPost]
    public IActionResult Post(Cliente cliente)
    {
        if (cliente is null) 
        {
            return BadRequest($"Dados inválidos");
        }

        var clienteNovo = _repository.Create(cliente);

        return new CreatedAtRouteResult("ObterCliente",
            new { id = clienteNovo.ClienteId }, clienteNovo);
    }

    [HttpPut("{id:int}")]
    public IActionResult Put(int id, Cliente cliente)
    {
        if (id != cliente.ClienteId)
        {
            return BadRequest($"Dados inválidos");
        }

        _repository.Update(cliente);

        return Ok(cliente);
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var cliente = _repository.Get(p => p.ClienteId == id);

        if (cliente is null)
        {
            return NotFound($"Não Encontrado");
        }

        var clienteDeletado = _repository.Delete(cliente);

        return Ok(clienteDeletado);
    }

}
