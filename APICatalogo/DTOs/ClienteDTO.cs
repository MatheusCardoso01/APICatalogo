using System.ComponentModel.DataAnnotations;

namespace APICatalogo.DTOs;

public class ClienteDTO
{
    public int ClienteId { get; set; }

    [Required]
    public string? Nome { get; set; }

    [Required]
    public int Idade { get; set; }

    [Required]
    public string? Sexo { get; set; }
}
