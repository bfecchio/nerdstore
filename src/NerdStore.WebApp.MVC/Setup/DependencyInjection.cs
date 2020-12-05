using MediatR;
using NerdStore.Core.Bus;
using NerdStore.Catalog.Data;
using NerdStore.Catalogo.Domain.Events;
using NerdStore.Catalogo.Domain.Services;
using NerdStore.Catalog.Data.Repositories;
using NerdStore.Catalogo.Domain.Repositories;
using NerdStore.Catalogo.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace NerdStore.WebApp.MVC.Setup
{
    public static class DependencyInjection
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            // Domain Bus (Mediator)
            services.AddScoped<IMediatrHandler, MediatrHandler>();

            // Catalogo
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<IProdutoAppService, ProdutoAppService>();
            services.AddScoped<IEstoqueService, EstoqueService>();
            services.AddScoped<CatalogoContext>();

            // Domain Events
            services.AddScoped<INotificationHandler<ProdutoAbaixoEstoqueEvent>, ProdutoEventHandler>();
        }
    }
}
