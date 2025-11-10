using APICatalogo.Validations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace APICatalogo.Models;

[Table("Produtos")]
public class Produto : IValidatableObject
{
    [Key]
    public int ProdutoId { get; set; }

    [Required]
    [StringLength(80, ErrorMessage = "O nome deve ter entre 2 e 80 caracteres", MinimumLength = 5)]
    [PrimeiraLetraMaiuscula] // Data Annotation customizada
    public string? Nome { get; set; }

    [Required]
    [StringLength(300)]
    public string? Descricao { get; set; }

    [Required]
    [Column(TypeName = "decimal(10,2)")]
    [Range(1, 10000, ErrorMessage = "O preço deve estar entre {1} e {2}")]
    public decimal Preco { get; set; }

    [Required]
    [StringLength(300)]
    public string? ImageUrl { get; set; }

    public int Estoque { get; set; }

    public DateTime DataCadastro { get; set; }

    public int CategoriaId { get; set; }

    [JsonIgnore]
    public Categoria? Categoria { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext) // Validação personalizada sem Data Annotation (com IValidatableObject)
    {
        if (!string.IsNullOrEmpty(this.Descricao))
        {
            var primeiraLetra = this.Descricao[0].ToString();

            if (primeiraLetra != primeiraLetra.ToUpper())
            {
                yield return new ValidationResult("A primeira letra deve ser maiuscula", new[] { nameof(this.Descricao) }); 
            }
        }

        if (this.Estoque <= 0) 
        {
            yield return new ValidationResult("O estoque deve ser maior que 0", new[] { nameof(this.Estoque) });
        }
    }
}

