using API.Dtos;
using AutoMapper;
using Core.Entidades;

namespace API
{
    public class MappingConfig : Profile
    {
        public MappingConfig() {
            CreateMap<Categoria, CategoriaDto>().ReverseMap();
            CreateMap<Categoria, CategoriaCreateDto>().ReverseMap();
            CreateMap<Categoria, CategoriaUpdateDto>().ReverseMap();

            CreateMap<Pais, PaisDto>().ReverseMap();
            CreateMap<Pais, PaisCreateDto>().ReverseMap();
            CreateMap<Pais, PaisUpdateDto>().ReverseMap();

            CreateMap<Lugar, LugarDto>().ReverseMap();
        }
    }
}
