using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using NerdStore.Core.Data;
using NerdStore.Core.Messages;
using NerdStore.Pagamentos.Data.Extensions;
using NerdStore.Pagamentos.Business.Models;
using NerdStore.Core.Communication.Mediator;

namespace NerdStore.Pagamentos.Data.Context
{
    public class PagamentoContext : DbContext, IUnitOfWork
    {
        #region Private Read-Only Fields

        private readonly IMediatorHandler _mediatorHandler;

        #endregion

        #region Public Properties

        public DbSet<Pagamento> Pagamentos { get; set; }
        public DbSet<Transacao> Transacoes { get; set; }

        #endregion

        #region Constructors

        public PagamentoContext(DbContextOptions<PagamentoContext> options, IMediatorHandler mediatorHandler)
            : base(options)
        {
            _mediatorHandler = mediatorHandler ?? throw new ArgumentNullException(nameof(mediatorHandler));
        }

        #endregion

        #region IUnitOfWork Members

        public async Task<bool> Commit()
        {
            foreach (var entry in ChangeTracker.Entries().Where(e => e.Entity.GetType().GetProperty("DataCadastro") != null))
            {
                if (entry.State == EntityState.Added)
                    entry.Property("DataCadastro").CurrentValue = DateTime.Now;

                if (entry.State == EntityState.Modified)
                    entry.Property("DataCadastro").IsModified = false;
            }

            var sucesso = await base.SaveChangesAsync() > 0;
            if (sucesso) await _mediatorHandler.PublicarEventos(this);

            return sucesso;
        }

        #endregion

        #region Overriden Methods

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
                x => x.GetProperties().Where(p => p.ClrType == typeof(string))))
            {
                property.SetColumnType("varchar(100)");
            }

            modelBuilder.Ignore<Event>();
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DbContext).Assembly);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(
                x => x.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;
            }
            
            base.OnModelCreating(modelBuilder);
        }

        #endregion
    }
}
