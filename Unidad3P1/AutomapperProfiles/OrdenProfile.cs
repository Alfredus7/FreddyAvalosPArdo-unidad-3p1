using AutoMapper;
using Unidad3P1.Data.Entidades;
using Unidad3P1.ViewModels;

namespace Unidad3P1.AutomapperProfiles
{
    public class OrdenProfile : Profile
    {
        public OrdenProfile()
        {
            CreateMap<OrdenEntity, OrdenViewModel>()
                .ForMember(dest => dest.OrdenId, opt => opt.MapFrom(src => src.OrdenId))
                .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Nombre))
                .ForMember(dest => dest.Apellido, opt => opt.MapFrom(src => src.Apellido))
                .ForMember(dest => dest.Direccion, opt => opt.MapFrom(src => src.Direccion))
                 .ForMember(dest => dest.Cantidad, opt => opt.MapFrom(src => src.Cantidad))
                  .ForMember(dest => dest.FechaOrden, opt => opt.MapFrom(src => src.FechaOrden))
                    .ForMember(dest => dest.Detalles, opt => opt.MapFrom(src => src.Detalles))
                .ReverseMap();
        }
    }
}
