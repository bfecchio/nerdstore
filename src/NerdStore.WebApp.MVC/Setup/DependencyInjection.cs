using MediatR;
using NerdStore.Core.Bus;
using NerdStore.Catalog.Data;
using NerdStore.Catalogo.Domain.Events;
using NerdStore.Catalogo.Domain.Services;
using NerdStore.Catalog.Data.Repositories;
using NerdStore.Vendas.Application.Commands;
using NerdStore.Catalogo.Domain.Repositories;
using NerdStore.Catalogo.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using NerdStore.Vendas.Application.Commands.Handlers;

namespace NerdStore.WebApp.MVC.Setup
{
    public static class DependencyInjection
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            // Domain Bus (Mediator)
            services.AddScoped<IMediatrHandler, MediatrHandler>();
                       
            // Catalogo
            services.AddScoped<CatalogoContext>();
            services.AddScoped<IEstoqueService, EstoqueService>();
            services.AddScoped<IProdutoAppService, ProdutoAppService>();
            services.AddScoped<IProdutoRepository, ProdutoRepository>();

            // Domain Events
            services.AddScoped<INotificationHandler<ProdutoAbaixoEstoqueEvent>, ProdutoEventHandler>();

            // Vendas
            services.AddScoped<IRequestHandler<AdicionarItemPedidoCommand, bool>, PedidoCommandHandler>();
        }
    }
}
