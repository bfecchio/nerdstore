using System;
using System.Linq;
using NerdStore.Core.Data;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using NerdStore.Catalogo.Domain.Models;
using NerdStore.Catalogo.Domain.Repositories;

namespace NerdStore.Catalog.Data.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        #region Private Read-Only Fields

        private readonly CatalogoContext _context;

        #endregion

        #region Constructors

        public ProdutoRepository(CatalogoContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        #endregion

        #region IProdutoRepository Members

        public IUnitOfWork UnitOfWork => _context;

        public async Task<IEnumerable<Produto>> ObterTodos()
        {
            return await _context.Produtos
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Produto> ObterPorId(Guid id)
        {
            return await _context.Produtos
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Produto>> ObterPorCategoria(int codigo)
        {
            return await _context.Produtos
                .AsNoTracking()
                .Include(i => i.Categoria)
                .Where(p => p.Categoria.Codigo == codigo)
                .ToListAsync();
        }

        public async Task<IEnumerable<Categoria>> ObterCategorias()
        {
            return await _context.Categorias
                .AsNoTracking()
                .ToListAsync();
        }

        public void Adicionar(Produto produto)
        {
            _context.Produtos.Add(produto);
        }

        public void Atualizar(Produto produto)
        {
            _context.Produtos.Update(produto);
        }

        public void Adicionar(Categoria categoria)
        {
            _context.Categorias.Add(categoria);
        }

        public void Atualizar(Categoria categoria)
        {
            _context.Categorias.Update(categoria);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }

        #endregion
    }
}
