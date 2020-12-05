using AutoMapper;
using NerdStore.Catalogo.Domain.Models;
using NerdStore.Catalogo.Application.ViewModels;

namespace NerdStore.Catalogo.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Produto, ProdutoViewModel>()
                .ForMember(dest => dest.Altura, opts => opts.MapFrom(src => src.Dimensoes.Altura))
                .ForMember(dest => dest.Largura, opts => opts.MapFrom(src => src.Dimensoes.Largura))
                .ForMember(dest => dest.Profundidade, opts => opts.MapFrom(src => src.Dimensoes.Profundidade));

            CreateMap<Categoria, CategoriaViewModel>();
        }
    }
}
