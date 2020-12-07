using System.Linq;
using System.Threading.Tasks;
using NerdStore.Core.DomainObjects;
using NerdStore.Vendas.Data.Context;
using NerdStore.Core.Communication.Mediator;

namespace NerdStore.Vendas.Data.Extensions
{
    public static class MediatorExtensions
    {
        //public static async Task PublicarEventos(this IMediatorHandler mediator, VendasContext ctx)
        //{
        //    var domainEntities = ctx.ChangeTracker
        //        .Entries<Entity>()
        //        .Where(x => x.Entity.Notificacoes != null && x.Entity.Notificacoes.Any());

        //    var domainEvents = domainEntities
        //        .SelectMany(x => x.Entity.Notificacoes)
        //        .ToList();

        //    domainEntities.ToList()
        //        .ForEach(entity => entity.Entity.LimparEventos());

        //    var tasks = domainEvents
        //        .Select(async (domainEvent) => {
        //            await mediator.PublicarEvento(domainEvent);
        //        });

        //    await Task.WhenAll(tasks);
        //}
    }
}
