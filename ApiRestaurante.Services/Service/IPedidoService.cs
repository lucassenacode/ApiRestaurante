using System.Collections.Generic;
using ApiRestaurante.Domain.Models;

namespace ApiRestaurante.Services.Service
{
    public interface IPedidoService
    {
        int CriarPedido(Pedido pedido);
        List<Pedido> ListarPedidos();
    }
}