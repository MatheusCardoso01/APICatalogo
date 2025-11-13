using APICatalogo.Models;
using APICatalogo.Validations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APICatalogo.DTOs
{
    public class ProdutoDTOUpdateResponse
    {
        public int ProdutoId { get; set; }

        public string? Nome { get; set; }

        public string? Descricao { get; set; }

        public decimal Preco { get; set; }

        public string? ImageUrl { get; set; }

        public int Estoque { get; set; }

        public DateTime DataCadastro { get; set; }

        public int CategoriaId { get; set; }
    }
}
