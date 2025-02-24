
using System.Collections.Generic;
using ApiRestaurante.Domain.Models;

namespace ApiRestaurante.Services.Service
{
    public interface IItemPedidoService
    {
        void AdicionarItemAoPedido(ItemPedido item);
        List<ItemPedido> ListarItensPedidoPorPedidoId(int pedidoId);
        void AtualizarItemPedido(ItemPedido item);
        void RemoverItemPedido(int id);
    }
}

