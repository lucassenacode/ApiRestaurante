
using System.Collections.Generic;
using ApiRestaurante.Domain.Models;

namespace ApiRestaurante.Services.Service
{
    public interface IItemPedidoService
    {
        List<ItemPedido> ListarItensPedidoPorPedidoId(int pedidoId);
        void AdicionarItemAoPedido(ItemPedido item);
        void AtualizarItemPedido(ItemPedido item);
        void RemoverItemPedido(int id);
    }
}

