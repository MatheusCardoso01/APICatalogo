using System.ComponentModel.DataAnnotations;

namespace APICatalogo.DTOs.Identity;

public class RegisterModel
{
    [Required(ErrorMessage = "Nome do usuário é obrigatório")]
    public string? Username { get; set; }

    [EmailAddress]
    [Required(ErrorMessage = "Email é obrigatório")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Senha é obrigatória")]
    public string? Password { get; set; }
}
