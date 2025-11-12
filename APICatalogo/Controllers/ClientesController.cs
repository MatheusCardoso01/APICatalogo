using APICatalogo.Models;
using APICatalogo.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APICatalogo.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClientesController : ControllerBase
{
    private readonly IUnityOfWork _uof;

    public ClientesController(IUnityOfWork uof)
    {
        _uof = uof;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Cliente>> GetAll()
    { 
        var clientes = _uof.ClienteRepository.GetAll();

        if (clientes is null)
        {
            return NotFound($"Não Encontrado");
        }

        return Ok(clientes);
    }

    [HttpGet("{id:int}", Name = "ObterCliente")]
    public ActionResult<Cliente> Get(int id)
    {
        var cliente = _uof.ClienteRepository.Get(c => c.ClienteId == id);

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

        var clienteNovo = _uof.ClienteRepository.Create(cliente);
        _uof.Commit();

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

        _uof.ClienteRepository.Update(cliente);
        _uof.Commit();

        return Ok(cliente);
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var cliente = _uof.ClienteRepository.Get(p => p.ClienteId == id);

        if (cliente is null)
        {
            return NotFound($"Não Encontrado");
        }

        var clienteDeletado = _uof.ClienteRepository.Delete(cliente);
        _uof.Commit();

        return Ok(clienteDeletado);
    }

}
