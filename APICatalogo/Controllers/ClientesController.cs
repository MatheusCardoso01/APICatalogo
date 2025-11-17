using APICatalogo.DTOs;
using APICatalogo.Models;
using APICatalogo.Pagination;
using APICatalogo.Repositories.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace APICatalogo.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClientesController : ControllerBase
{
    private readonly IUnityOfWork _uof;
    private readonly IMapper _mapper;

    public ClientesController(IUnityOfWork uof, IMapper mapper)
    {
        _uof = uof;
        _mapper = mapper;
    }

    [HttpGet]
    public ActionResult<IEnumerable<ClienteDTO>> GetAll()
    { 
        var clientes = _uof.ClienteRepository.GetAll();

        if (clientes is null)
        {
            return NotFound($"Não Encontrado");
        }

        var clientesDTO = _mapper.Map<IEnumerable<ClienteDTO>>(clientes);

        return Ok(clientesDTO);
    }

    [HttpGet("{id:int}", Name = "ObterCliente")]
    public ActionResult<ClienteDTO> Get(int id)
    {
        var cliente = _uof.ClienteRepository.Get(c => c.ClienteId == id);

        if (cliente is null)
        {
            return NotFound($"Não Encontrado");
        }

        var clienteDTO = _mapper.Map<ClienteDTO>(cliente);

        return Ok(clienteDTO);
    }

    [HttpPost]
    public ActionResult<ClienteDTO> Post(ClienteDTO clienteDTO)
    {
        if (clienteDTO is null) 
        {
            return BadRequest($"Dados inválidos");
        }

        var cliente = _mapper.Map<Cliente>(clienteDTO);

        var clienteNovo = _uof.ClienteRepository.Create(cliente);
        _uof.Commit();

        var clienteNovoDTO = _mapper.Map<ClienteDTO>(clienteNovo);

        return new CreatedAtRouteResult("ObterCliente",
            new { id = clienteNovoDTO.ClienteId }, clienteNovoDTO);
    }

    [HttpPatch("{id:int}/UpdatePartial")]
    public ActionResult<ClienteDTOUpdateResponse> Patch(int id, JsonPatchDocument<ClienteDTOUpdateRequest> patchClienteDTO)
    {
        if (patchClienteDTO is null || id <= 0)
        { 
            return BadRequest("Dados inválidos");
        }

        var cliente = _uof.ClienteRepository.Get(c => c.ClienteId == id);

        if (cliente is null) return NotFound($"Não Encontrado");

        var clienteUpdateRequest = _mapper.Map<ClienteDTOUpdateRequest>(cliente);

        patchClienteDTO.ApplyTo(clienteUpdateRequest, ModelState);

        if (!ModelState.IsValid || !TryValidateModel(clienteUpdateRequest))
        { 
            return BadRequest(ModelState);
        }

        _mapper.Map(clienteUpdateRequest, cliente);

        _uof.ClienteRepository.Update(cliente);
        _uof.Commit();

        return Ok(_mapper.Map<ClienteDTOUpdateResponse>(cliente));
    }

    [HttpPut("{id:int}")]
    public ActionResult<ClienteDTO> Put(int id, ClienteDTO clienteDTO)
    {
        if (id != clienteDTO.ClienteId)
        {
            return BadRequest($"Dados inválidos");
        }

        var cliente = _mapper.Map<Cliente>(clienteDTO);

        var clienteAtualizado = _uof.ClienteRepository.Update(cliente);
        _uof.Commit();

        var clienteAtualizadoDTO = _mapper.Map<ClienteDTO>(clienteAtualizado);

        return Ok(clienteAtualizadoDTO);
    }

    [HttpDelete("{id:int}")]
    public ActionResult<ClienteDTO> Delete(int id)
    {
        var cliente = _uof.ClienteRepository.Get(p => p.ClienteId == id);

        if (cliente is null)
        {
            return NotFound($"Não Encontrado");
        }

        var clienteDeletado = _uof.ClienteRepository.Delete(cliente);
        _uof.Commit();

        var clienteDeletadoDTO = _mapper.Map<ClienteDTO>(clienteDeletado);

        return Ok(clienteDeletado);
    }

    [HttpGet("pagination")]
    public ActionResult<IEnumerable<ClienteDTO>> GetClientes([FromQuery] Parameters clientesParams)
    { 
        var clientes = _uof.ClienteRepository.GetClientes(clientesParams);

        if (clientes is null)
        {
            return NotFound($"Não Encontrado");
        }

        var metadata = new
        {
            clientes.TotalCount,
            clientes.PageSize,
            clientes.CurrentPage,
            clientes.TotalPages,
            clientes.HasNext,
            clientes.HasPrevious
        };

        Response.Headers.Add("X-Pagination", System.Text.Json.JsonSerializer.Serialize(metadata));

        var clientesDTO = _mapper.Map<IEnumerable<ClienteDTO>>(clientes);

        return Ok(clientesDTO);
    }

}
