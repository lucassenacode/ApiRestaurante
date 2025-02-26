using System.Collections.Generic;
using ApiRestaurante.Domain.Models;
using ApiRestaurante.Domain.Models.Enuns;
using ApiRestaurante.Domain.Models.Exceptions;
using ApiRestaurante.Repositories.Repository;


namespace ApiRestaurante.Services.Service
{
    public class ItemPedidoService : IItemPedidoService
    {

        private readonly IItemPedidoRepository _repositorio;

        public ItemPedidoService(IItemPedidoRepository repositorio)
        {
            _repositorio = repositorio;
        }

        public void AdicionarItemAoPedido(ItemPedido item)
        {
            ValidarItemPedido(item);
            ((Contexto)_repositorio).AbrirConexao();
            try
            {
                _repositorio.InserirItemPedido(item);
            }
            finally
            {
                ((Contexto)_repositorio).FecharConexao();
            }
        }

        public List<ItemPedido> ListarItensPedidoPorPedidoId(int pedidoId)
        {
            ((Contexto)_repositorio).AbrirConexao();
            try
            {
                return _repositorio.ListarItensPedidoPorPedidoId(pedidoId);
            }
            finally
            {
                ((Contexto)_repositorio).FecharConexao();
            }
        }

        public void AtualizarItemPedido(ItemPedido item)
        {
            ((Contexto)_repositorio).AbrirConexao();
            try
            {
                _repositorio.AtualizarItemPedido(item);
            }
            finally
            {
                ((Contexto)_repositorio).FecharConexao();
            }
        }

        public void RemoverItemPedido(int id)
        {
            ((Contexto)_repositorio).AbrirConexao();
            try
            {
                _repositorio.RemoverItemPedido(id);
            }
            finally
            {
                ((Contexto)_repositorio).FecharConexao();
            }
        }

        private void ValidarItemPedido(ItemPedido item)
        {
            if (item == null)
            {
                throw new ValidacaoException("O JSON está mal formatado ou foi enviado vazio.");
            }

            if (item.IdPedido <= 0)
            {
                throw new ValidacaoException("O ID do pedido é obrigatório e deve ser maior que zero.");
            }

            if (item.IdProduto <= 0)
            {
                throw new ValidacaoException("O ID do produto é obrigatório e deve ser maior que zero.");
            }

            if (item.Quantidade <= 0)
            {
                throw new ValidacaoException("A quantidade é obrigatória e deve ser maior que zero.");
            }
        }
    }
}