using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiRestaurante.Domain.Models;

namespace ApiRestaurante.Repositories.Repository
{
    public interface IItemPedidoRepository
    {
        void InserirItemPedido(ItemPedido item);
        List<ItemPedido> ListarItensPorPedido(int pedidoId);
        List<ItemPedido> ListarItensPedidoPorPedidoId(int pedidoId);
        void AtualizarItemPedido(ItemPedido item);
        void RemoverItemPedido(int id);
    }
}