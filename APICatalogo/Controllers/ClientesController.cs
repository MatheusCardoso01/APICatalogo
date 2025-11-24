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
[ApiConventionType(typeof(DefaultApiConventions))]
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
    public async Task<ActionResult<IEnumerable<ClienteDTO>>> GetAllAsync()
    { 
        var clientes = await _uof.ClienteRepository.GetAllAsync();

        if (clientes is null)
        {
            return NotFound($"Não Encontrado");
        }

        var clientesDTO = _mapper.Map<IEnumerable<ClienteDTO>>(clientes);

        return Ok(clientesDTO);
    }

    [HttpGet("{id:int}", Name = "ObterCliente")]
    public async Task<ActionResult<ClienteDTO>> GetAsync(int id)
    {
        var cliente = await _uof.ClienteRepository.GetAsync(c => c.ClienteId == id);

        if (cliente is null)
        {
            return NotFound($"Não Encontrado");
        }

        var clienteDTO = _mapper.Map<ClienteDTO>(cliente);

        return Ok(clienteDTO);
    }

    [HttpPost]
    public async Task<ActionResult<ClienteDTO>> PostAsync(ClienteDTO clienteDTO)
    {
        if (clienteDTO is null) 
        {
            return BadRequest($"Dados inválidos");
        }

        var cliente = _mapper.Map<Cliente>(clienteDTO);

        var clienteNovo = _uof.ClienteRepository.Create(cliente);

        await _uof.CommitAsync();

        var clienteNovoDTO = _mapper.Map<ClienteDTO>(clienteNovo);

        return new CreatedAtRouteResult("ObterCliente",
            new { id = clienteNovoDTO.ClienteId }, clienteNovoDTO);
    }

    [HttpPatch("{id:int}/UpdatePartial")]
    public async Task<ActionResult<ClienteDTOUpdateResponse>> PatchAsync(int id, JsonPatchDocument<ClienteDTOUpdateRequest> patchClienteDTO)
    {
        if (patchClienteDTO is null || id <= 0)
        { 
            return BadRequest("Dados inválidos");
        }

        var cliente = await _uof.ClienteRepository.GetAsync(c => c.ClienteId == id);

        if (cliente is null) return NotFound($"Não Encontrado");

        var clienteUpdateRequest = _mapper.Map<ClienteDTOUpdateRequest>(cliente);

        patchClienteDTO.ApplyTo(clienteUpdateRequest, ModelState);

        if (!ModelState.IsValid || !TryValidateModel(clienteUpdateRequest))
        { 
            return BadRequest(ModelState);
        }

        _mapper.Map(clienteUpdateRequest, cliente);

        _uof.ClienteRepository.Update(cliente);

        await _uof.CommitAsync();

        return Ok(_mapper.Map<ClienteDTOUpdateResponse>(cliente));
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<ClienteDTO>> PutAsync(int id, ClienteDTO clienteDTO)
    {
        if (id != clienteDTO.ClienteId)
        {
            return BadRequest($"Dados inválidos");
        }

        var cliente = _mapper.Map<Cliente>(clienteDTO);

        var clienteAtualizado = _uof.ClienteRepository.Update(cliente);
        await _uof.CommitAsync();

        var clienteAtualizadoDTO = _mapper.Map<ClienteDTO>(clienteAtualizado);

        return Ok(clienteAtualizadoDTO);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<ClienteDTO>> DeleteAsync(int id)
    {
        var cliente = await _uof.ClienteRepository.GetAsync(p => p.ClienteId == id);

        if (cliente is null)
        {
            return NotFound($"Não Encontrado");
        }

        var clienteDeletado = _uof.ClienteRepository.Delete(cliente);

        await _uof.CommitAsync();

        var clienteDeletadoDTO = _mapper.Map<ClienteDTO>(clienteDeletado);

        return Ok(clienteDeletado);
    }

    [HttpGet("pagination")]
    public async Task<ActionResult<IEnumerable<ClienteDTO>>> GetClientesAsync([FromQuery] Parameters clientesParams)
    {        
        var clientes = await _uof.ClienteRepository.GetClientesAsync(clientesParams);

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
