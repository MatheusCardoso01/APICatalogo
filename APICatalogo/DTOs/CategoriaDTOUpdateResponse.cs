using System.ComponentModel.DataAnnotations;

namespace APICatalogo.DTOs;

public class CategoriaDTOUpdateResponse
{
    public int CategoriaId { get; set; }

    public string? Nome { get; set; }

    public string? ImageUrl { get; set; }

}
