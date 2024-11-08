using AutoMapper;
using Unidad3P1.Data.Entidades;
using webApi.Dtos;


namespace webApi.AutomapperProfiles
{
    public class CategoryProfileApi : Profile
    {   
        public CategoryProfileApi() 
        {
            CreateMap<CategoriaEntity, CategoryDtos>()
                .ForMember(dest => dest.CategoriaId, opt => opt.MapFrom(src => src.CategoriaId))
                .ForMember(dest => dest.CategoriaNombre, opt => opt.MapFrom(src => src.CategoriaNombre))
                .ForMember(dest => dest.Descripcion, opt => opt.MapFrom(src => src.Descripcion))
                .ReverseMap();
        }

    }
}
