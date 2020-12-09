using MediatR;
using Microsoft.Extensions.DependencyInjection;

using NerdStore.Core.Communication.Mediator;
using NerdStore.Core.Messages.CommonMessages.Notifications;

using NerdStore.Catalog.Data;
using NerdStore.Catalogo.Domain.Events;
using NerdStore.Catalogo.Domain.Services;
using NerdStore.Catalog.Data.Repositories;
using NerdStore.Catalogo.Domain.Repositories;
using NerdStore.Catalogo.Application.Services;

using NerdStore.Vendas.Data.Context;
using NerdStore.Vendas.Data.Repositories;
using NerdStore.Vendas.Application.Events;
using NerdStore.Vendas.Application.Queries;
using NerdStore.Vendas.Domain.Repositories;
using NerdStore.Vendas.Application.Commands;
using NerdStore.Vendas.Application.Events.Handlers;
using NerdStore.Vendas.Application.Commands.Handlers;

namespace NerdStore.WebApp.MVC.Setup
{
    public static class DependencyInjection
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            // Mediator and messages
            services.AddScoped<IMediatorHandler, MediatorHandler>();
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();
                       
            // Catalogo
            services.AddScoped<CatalogoContext>();
            services.AddScoped<IEstoqueService, EstoqueService>();
            services.AddScoped<IProdutoAppService, ProdutoAppService>();
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            
            services.AddScoped<INotificationHandler<ProdutoAbaixoEstoqueEvent>, ProdutoEventHandler>();

            // Vendas
            services.AddScoped<VendasContext>();
            services.AddScoped<IPedidoQueries, PedidoQueries>();
            services.AddScoped<IPedidoRepository, PedidoRepository>();

            services.AddScoped<IRequestHandler<AdicionarItemPedidoCommand, bool>, PedidoCommandHandler>();
            services.AddScoped<IRequestHandler<AtualizarItemPedidoCommand, bool>, PedidoCommandHandler>();
            services.AddScoped<IRequestHandler<RemoverItemPedidoCommand, bool>, PedidoCommandHandler>();
            services.AddScoped<IRequestHandler<AplicarVoucherPedidoCommand, bool>, PedidoCommandHandler>();

            services.AddScoped<INotificationHandler<PedidoAtualizadoEvent>, PedidoEventHandler>();
            services.AddScoped<INotificationHandler<PedidoItemAdicionadoEvent>, PedidoEventHandler>();
            services.AddScoped<INotificationHandler<PedidoItemAtualizadoEvent>, PedidoEventHandler>();
            services.AddScoped<INotificationHandler<PedidoItemRemovidoEvent>, PedidoEventHandler>();
            services.AddScoped<INotificationHandler<PedidoVoucherAplicadoEvent>, PedidoEventHandler>();
            services.AddScoped<INotificationHandler<PedidoRascunhoIniciadoEvent>, PedidoEventHandler>();
        }
    }
}
