using AutoMapper;
using Unidad3P1.Data.Entidades;
using webApi.Dtos;


namespace webApi.AutomapperProfiles
{
    public class ProductoProfileApi : Profile
    {   
        public ProductoProfileApi() 
        {
            CreateMap<ProductoEntity, ProductoDtos>()
                 .ForMember(dest => dest.ProductoId, opt => opt.MapFrom(src => src.ProductoId))
                 .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Nombre))
                 .ForMember(dest => dest.DescripcionCorta, opt => opt.MapFrom(src => src.DescripcionCorta))
                 .ForMember(dest => dest.DescripcionLarga, opt => opt.MapFrom(src => src.DescripcionLarga))
                 .ForMember(dest => dest.Precio, opt => opt.MapFrom(src => src.Precio))
                 .ForMember(dest => dest.ImagenUrl, opt => opt.MapFrom(src => src.ImagenUrl))
                 .ForMember(dest => dest.Stock, opt => opt.MapFrom(src => src.InStock))
                 .ForMember(dest => dest.CategoriaId, opt => opt.MapFrom(src => src.CategoriaId))
                 .ForMember(dest => dest.Categoria, opt => opt.MapFrom(src => src.Categoria))
                 .ReverseMap();
        }

    }
}
