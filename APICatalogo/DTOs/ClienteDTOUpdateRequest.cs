using APICatalogo.Validations;
using System.ComponentModel.DataAnnotations;

namespace APICatalogo.DTOs;

public class ClienteDTOUpdateRequest
{
    public string? Nome { get; set; }

    [Range(0, 120, ErrorMessage = "A idade deve ser um número positivo")]
    public int Idade { get; set; }

    [PermissoesDeSexo]
    public string? Sexo { get; set; }
}
