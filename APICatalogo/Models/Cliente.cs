using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APICatalogo.Models;

[Table("Clientes")]
public class Cliente
{
    [Key]
    public int ClienteId { get; set; }

    [Required]
    public string? Nome { get; set; }

    [Required]
    public int Idade { get; set; }

    [Required]
    public string? Sexo { get; set; }
}
