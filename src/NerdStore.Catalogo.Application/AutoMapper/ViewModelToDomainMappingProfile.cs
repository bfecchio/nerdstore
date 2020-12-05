using AutoMapper;
using NerdStore.Catalogo.Domain.Models;
using NerdStore.Catalogo.Application.ViewModels;

namespace NerdStore.Catalogo.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<CategoriaViewModel, Categoria>()
                .ConstructUsing(ctor => new Categoria(ctor.Nome, ctor.Codigo));

            CreateMap<ProdutoViewModel, Produto>()
                .ConstructUsing(ctor
                    => new Produto(ctor.Nome, ctor.Descricao, ctor.Ativo, ctor.Valor, ctor.CategoriaId, ctor.DataCadastro, ctor.Imagem
                        , new Dimensoes(ctor.Altura, ctor.Largura, ctor.Profundidade)
                    )
                );
        }
    }
}
