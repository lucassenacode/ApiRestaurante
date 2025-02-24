using System.Collections.Generic;
using ApiRestaurante.Domain.Models;
using ApiRestaurante.Domain.Models.Enuns;

namespace ApiRestaurante.Services.Service
{
    public interface IPedidoService
    {
        List<Pedido> ListarPedidos();
        Pedido ObterPedidoPorId(int idPedido);
        int CriarPedido(Pedido pedido);
        void AtualizarPedido(Pedido pedido);
        void AtualizarStatusPedido(int id, StatusPedido novoStatus);
        List<Pedido> ObterPedidosPorStatus(StatusPedido status);
        List<Pedido> ObterPedidosFinalizados();
        void DeletarPedido(int idPedido);

    }
}