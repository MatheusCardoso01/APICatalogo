using System.ComponentModel.DataAnnotations;

namespace APICatalogo.DTOs;

public class ClienteDTOUpdateResponse
{
    public int ClienteId { get; set; }

    public string? Nome { get; set; }

    public int Idade { get; set; }

    public string? Sexo { get; set; }
}
