using System.ComponentModel.DataAnnotations;

namespace APICatalogo.DTOs.Identity;

public class LoginModel
{
    [Required(ErrorMessage = "Nome do usuário é obrigatório")]
    public string? UserName { get; set; }

    [Required(ErrorMessage = "Senha é obrigatória")]
    public string? Password { get; set; }
}
