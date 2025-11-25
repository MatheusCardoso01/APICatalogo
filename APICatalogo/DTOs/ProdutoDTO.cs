using APICatalogo.Validations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APICatalogo.DTOs;

public class ProdutoDTO
{
    public int ProdutoId { get; set; }

    [Required]
    [StringLength(80, ErrorMessage = "O nome deve ter entre 2 e 80 caracteres", MinimumLength = 5)]
    [PrimeiraLetraMaiuscula] // Data Annotation customizada
    public string? Nome { get; set; }

    [Required]
    [StringLength(300)]
    public string? Descricao { get; set; }

    [Required]
    [Range(1, 10000, ErrorMessage = "O preço deve estar entre {1} e {2}")]
    public decimal Preco { get; set; }

    [Required]
    [StringLength(300)]
    public string? ImageUrl { get; set; }

    [Required]
    public int CategoriaId { get; set; } // <-- Adicione este campo!
}
