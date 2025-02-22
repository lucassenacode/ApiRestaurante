
using System.Collections.Generic;
using ApiRestaurante.Domain.Models;

namespace ApiRestaurante.Services.Service
{
    public interface IItemPedidoService
    {
        void AdicionarItemAoPedido(ItemPedido item);
        List<ItemPedido> ListarItensPorPedido(int pedidoId);
    }
}

