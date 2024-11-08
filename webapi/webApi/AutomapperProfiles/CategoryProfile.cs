using AutoMapper;
using Unidad3P1.Data.Entidades;
using Unidad3P1.ViewModels;


namespace Unidad3P1.AutomapperProfiles
{
    public class CategoryProfile : Profile
    {   
        public CategoryProfile() 
        {
            CreateMap<CategoriaEntity, CategoryDtos>()
                .ForMember(dest => dest.CategoriaId, opt => opt.MapFrom(src => src.CategoriaId))
                .ForMember(dest => dest.CategoriaNombre, opt => opt.MapFrom(src => src.CategoriaNombre))
                .ForMember(dest => dest.Descripcion, opt => opt.MapFrom(src => src.Descripcion))
                .ReverseMap();
        }

    }
}
