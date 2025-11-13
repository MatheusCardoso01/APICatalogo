using APICatalogo.Models;
using AutoMapper;

namespace APICatalogo.DTOs.Mappings;

public class AutoMapperDTOMappingProfile : Profile
{
    public AutoMapperDTOMappingProfile()
    {
        // CreateMap<Categoria, CategoriaDTO>().ReverseMap();
        CreateMap<Produto, ProdutoDTO>().ReverseMap();
        CreateMap<Cliente, ClienteDTO>().ReverseMap();
        CreateMap<Produto, ProdutoDTOUpdateRequest>().ReverseMap();
        CreateMap<Produto, ProdutoDTOUpdateResponse>().ReverseMap();
    }
}