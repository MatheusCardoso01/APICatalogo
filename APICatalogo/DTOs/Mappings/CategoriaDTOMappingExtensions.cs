using APICatalogo.Models;
using System.Runtime.CompilerServices;

namespace APICatalogo.DTOs.Mappings;

public static class CategoriaDTOMappingExtensions
{
    // Extension Methods podem ser chamados em qualquer lugar do projeto desde que o objeto seja do tipo do método de extensão
    public static CategoriaDTO? ToCategoriaDTO(this Categoria categoria)
    {
        if (categoria is null)
        {
            return null;
        }

        return new CategoriaDTO
        {
            CategoriaId = categoria.CategoriaId,
            Nome = categoria.Nome,
            ImageUrl = categoria.ImageUrl
        };
    }

    public static Categoria? ToCategoria(this CategoriaDTO categoriaDTO)
    {
        if (categoriaDTO is null) return null;

        return new Categoria
        {
            CategoriaId = categoriaDTO.CategoriaId,
            Nome = categoriaDTO.Nome,
            ImageUrl = categoriaDTO.ImageUrl
        };
    }

    public static IEnumerable<CategoriaDTO> ToCategoriaDTOList(this IEnumerable<Categoria> categorias)
    {
        if (categorias is null || !categorias.Any())
        {
            return new List<CategoriaDTO>();
        }

        return categorias.Select(categoria => new CategoriaDTO
        {
            CategoriaId = categoria.CategoriaId,
            Nome = categoria.Nome,
            ImageUrl = categoria.ImageUrl
        }).ToList();
    }

    public static CategoriaDTOUpdateRequest? ToCategoriaUpdateRequest(this Categoria categoria)
    { 
        if (categoria is null) return null;

        return new CategoriaDTOUpdateRequest
        {
            Nome = categoria.Nome,
            ImageUrl = categoria.ImageUrl
        };
    }


    public static Categoria? UpdateCategoriaFromCategoriaUpdateRequest(this Categoria categoria, CategoriaDTOUpdateRequest categoriaUpdateRequest)
    { 
        if (!string.IsNullOrEmpty(categoriaUpdateRequest.Nome))
        { 
            categoria.Nome = categoriaUpdateRequest.Nome;        
        }

        if (!string.IsNullOrEmpty(categoriaUpdateRequest.ImageUrl))
        { 
            categoria.ImageUrl = categoriaUpdateRequest.ImageUrl;
        }

        return categoria;
    }
    public static CategoriaDTOUpdateResponse? ToCategoriaUpdateResponse(this Categoria categoria)
    { 
        if (categoria is null) return null;

        return new CategoriaDTOUpdateResponse
        {
            CategoriaId = categoria.CategoriaId,
            Nome = categoria.Nome,
            ImageUrl = categoria.ImageUrl
        };
    }

}