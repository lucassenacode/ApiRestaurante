using System.Collections.Generic;
using ApiRestaurante.Domain.Models;

namespace ApiRestaurante.Repositories.Repository
{
    public interface IPedidoRepository
    {
        int CriarPedido(Pedido pedido);
        List<Pedido> ListarPedidos();
    }
}