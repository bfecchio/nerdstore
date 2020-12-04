using Microsoft.EntityFrameworkCore;
using NerdStore.Catalogo.Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace NerdStore.Catalog.Data.Mappings
{
    public class ProdutoMapping : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.ToTable("Produtos");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Nome)
                .IsRequired()
                .HasColumnType("varchar(250)");
            builder.Property(c => c.Descricao)
                .IsRequired()
                .HasColumnType("varchar(500)");
            builder.Property(c => c.Imagem)
                .IsRequired()
                .HasColumnType("varchar(250)");

            builder.OwnsOne(c => c.Dimensoes, cm =>
            {
                cm.Property(c => c.Altura)
                    .HasColumnName(nameof(Dimensoes.Altura))
                    .HasColumnType("int");

                cm.Property(c => c.Largura)
                    .HasColumnName(nameof(Dimensoes.Largura))
                    .HasColumnType("int");

                cm.Property(c => c.Profundidade)
                    .HasColumnName(nameof(Dimensoes.Profundidade))
                    .HasColumnType("int");
            });
        }
    }
}
