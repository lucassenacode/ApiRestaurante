using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiRestaurante.Domain.Models;
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
    }
}