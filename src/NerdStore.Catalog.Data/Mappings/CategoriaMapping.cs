using Microsoft.EntityFrameworkCore;
using NerdStore.Catalogo.Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace NerdStore.Catalog.Data.Mappings
{
    public class CategoriaMapping : IEntityTypeConfiguration<Categoria>
    {
        public void Configure(EntityTypeBuilder<Categoria> builder)
        {
            builder.ToTable("Categorias");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Nome)
                .IsRequired()
                .HasColumnType("varchar(250)");
            builder.Property(c => c.Codigo)
                .IsRequired()
                .HasColumnType("int");

            builder.HasMany(c => c.Produtos)
                .WithOne(p => p.Categoria)
                .HasForeignKey(p => p.CategoriaId);
        }
    }
}
