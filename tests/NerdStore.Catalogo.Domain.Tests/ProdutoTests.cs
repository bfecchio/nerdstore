using Xunit;
using System;
using NerdStore.Core.DomainObjects;
using NerdStore.Catalogo.Domain.Models;

namespace NerdStore.Catalogo.Domain.Tests
{
    public class ProdutoTests
    {
        [Fact]
        public void Produto_Validar_ValidacoesDevemRetornarExceptions()
        {
            var ex = Assert.Throws<DomainException>(() =>
                new Produto(string.Empty, "Descricao", false, 100, Guid.NewGuid(), DateTime.Now, "Imagem", new Dimensoes(1, 1, 1))
            );
            Assert.Equal("O campo Nome do Produto não pode ser vazio.", ex.Message);

            ex = Assert.Throws<DomainException>(() =>
                new Produto("Nome", string.Empty, false, 100, Guid.NewGuid(), DateTime.Now, "Imagem", new Dimensoes(1, 1, 1))
            );
            Assert.Equal("O campo Descricao do Produto não pode ser vazio.", ex.Message);

            ex = Assert.Throws<DomainException>(() =>
                new Produto("Nome", "Descricao", false, 0, Guid.NewGuid(), DateTime.Now, "Imagem", new Dimensoes(1, 1, 1))
            );
            Assert.Equal("O campo Valor do Produto não pode ser menor que um.", ex.Message);

            ex = Assert.Throws<DomainException>(() =>
                new Produto("Nome", "Descricao", false, 100, Guid.Empty, DateTime.Now, "Imagem", new Dimensoes(1, 1, 1))
            );
            Assert.Equal("O campo CategoriaId do Produto não pode estar vazio.", ex.Message);

            ex = Assert.Throws<DomainException>(() =>
                new Produto("Nome", "Descricao", false, 100, Guid.NewGuid(), DateTime.Now, string.Empty, new Dimensoes(1, 1, 1))
            );
            Assert.Equal("O campo Imagem do Produto não pode estar vazio.", ex.Message);

            ex = Assert.Throws<DomainException>(() =>
                new Produto("Nome", "Descricao", false, 100, Guid.NewGuid(), DateTime.Now, "Imagem", new Dimensoes(0, 1, 1))
            );
            Assert.Equal("O campo Altura não pode ser menor ou igual a zero.", ex.Message);

            ex = Assert.Throws<DomainException>(() =>
                new Produto("Nome", "Descricao", false, 100, Guid.NewGuid(), DateTime.Now, "Imagem", new Dimensoes(1, 0, 1))
            );
            Assert.Equal("O campo Largura não pode ser menor ou igual a zero.", ex.Message);

            ex = Assert.Throws<DomainException>(() =>
                new Produto("Nome", "Descricao", false, 100, Guid.NewGuid(), DateTime.Now, "Imagem", new Dimensoes(1, 1, 0))
            );
            Assert.Equal("O campo Profundidade não pode ser menor ou igual a zero.", ex.Message);
        }
    }
}
