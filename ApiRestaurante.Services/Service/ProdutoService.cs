using System.Collections.Generic;
using ApiRestaurante.Domain.Models;
using ApiRestaurante.Domain.Models.Enuns;
using ApiRestaurante.Domain.Models.Exceptions;
using ApiRestaurante.Repositories.Repository;

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

        public Produto ObterProdutoPorId(int idProduto)
        {
            try
            {
                ((Contexto)_repositorio).AbrirConexao();
                return _repositorio.ObterProdutoPorId(idProduto);
            }
            finally
            {
                ((Contexto)_repositorio).FecharConexao();
            }
        }

        public void CriarProduto(Produto produto)
        {
            try
            {
                ValidarProduto(produto);
                ((Contexto)_repositorio).AbrirConexao();
                _repositorio.CriarProduto(produto);
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
        private void ValidarProduto(Produto produto)
        {
            if (produto == null)
            {
                throw new ValidacaoException("O JSON está mal formatado ou foi enviado vazio.");
            }

            if (string.IsNullOrWhiteSpace(produto.NomeProduto))
            {
                throw new ValidacaoException("O nome do produto é obrigatório.");
            }

            if (produto.NomeProduto.Trim().Length < 3 || produto.NomeProduto.Trim().Length > 100)
            {
                throw new ValidacaoException("O nome do produto precisa ter entre 3 e 100 caracteres.");
            }

            if (produto.Preco <= 0)
            {
                throw new ValidacaoException("O preço do produto deve ser maior que zero.");
            }

            if (!Enum.IsDefined(typeof(TipoProduto), produto.TipoProduto))
            {
                throw new ValidacaoException("O tipo de produto é inválido.");
            }
        }
    }
}