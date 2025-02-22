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

        public void InserirProduto(Produto produto)
        {
            try
            {
                ((Contexto)_repositorio).AbrirConexao();
                _repositorio.InserirProduto(produto);
            }
            finally
            {
                ((Contexto)_repositorio).FecharConexao();
            }
        }

        public void AtualizarProduto(Produto produto)
        {

            try
            {
                ((Contexto)_repositorio).AbrirConexao();

                if (!_repositorio.ProdutoExiste(produto.IdProduto))
                {
                    throw new Exception($"Produto de ID: {produto.IdProduto} não encontrado");
                }
                _repositorio.AtualizarProduto(produto);
            }
            finally
            {
                ((Contexto)_repositorio).FecharConexao();
            }
        }

        public void DeletarProduto(int idProduto)
        {
            try
            {
                ((Contexto)_repositorio).AbrirConexao();
                if (!_repositorio.ProdutoExiste(idProduto))
                {
                    throw new Exception($"Produto de ID: {idProduto} não encontrado");
                }
                _repositorio.DeletarProduto(idProduto);
            }
            finally
            {
                ((Contexto)_repositorio).FecharConexao();
            }
        }
    }
}