using APICatalogo.Models;
using AutoMapper;

namespace APICatalogo.DTOs.Mappings;

public class AutoMapperDTOMappingProfile : Profile
{
    // progremadores experientes não recomendam o automapper, mas sim criar o próprio mapeamento manualmente
    public AutoMapperDTOMappingProfile()
    {
        // CreateMap<Categoria, CategoriaDTO>().ReverseMap();
        CreateMap<Produto, ProdutoDTO>().ReverseMap();
        CreateMap<Cliente, ClienteDTO>().ReverseMap();
        CreateMap<Produto, ProdutoDTOUpdateRequest>().ReverseMap();
        CreateMap<Produto, ProdutoDTOUpdateResponse>().ReverseMap();
        CreateMap<Cliente, ClienteDTOUpdateRequest>().ReverseMap();
        CreateMap<Cliente, ClienteDTOUpdateResponse>().ReverseMap();
    }
}