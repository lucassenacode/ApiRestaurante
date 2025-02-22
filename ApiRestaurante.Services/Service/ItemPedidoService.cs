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

        public List<ItemPedido> ListarItensPorPedido(int pedidoId)
        {
            ((Contexto)_repositorio).AbrirConexao();
            try
            {
                return _repositorio.ListarItensPorPedido(pedidoId);
            }
            finally
            {
                ((Contexto)_repositorio).FecharConexao();
            }
        }

    }
}