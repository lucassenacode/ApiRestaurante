using System.Collections.Generic;
using ApiRestaurante.Domain.Models;
using ApiRestaurante.Repositories.Repository;
using ApiRestaurante.Services.Service;

namespace ApiRestaurante.Services.Service
{
    public class ProdutoService : IProdutoService
    {
        private readonly IProdutoRepository _repositorio;
        public ProdutoService(IProdutoRepository repositorio)
        {
            _repositorio = repositorio;
        }

        public List<Produto> ListarProdutos()
        {
            try
            {
                ((Contexto)_repositorio).AbrirConexao();
                return _repositorio.ListarProdutos();
            }
            finally
            {
                ((Contexto)_repositorio).FecharConexao();
            }
        }
    }
}