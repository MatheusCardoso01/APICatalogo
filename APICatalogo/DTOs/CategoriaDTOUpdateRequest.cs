using System.ComponentModel.DataAnnotations;

namespace APICatalogo.DTOs;

public class CategoriaDTOUpdateRequest
{
    public string? Nome { get; set; }

    public string? ImageUrl { get; set; }
}
