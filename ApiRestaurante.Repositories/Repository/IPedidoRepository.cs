using System.Collections.Generic;
using ApiRestaurante.Domain.Models;
using ApiRestaurante.Domain.Models.Enuns;

namespace ApiRestaurante.Repositories.Repository
{
    public interface IPedidoRepository
    {
        List<Pedido> ListarPedidos();
        Pedido ObterPedidoPorId(int id);
        int CriarPedido(Pedido pedido);
        void AtualizarPedido(Pedido pedido);
        void DeletarPedido(int id);
        void AtualizarStatusPedido(int id, StatusPedido novoStatus);
        List<Pedido> ObterPedidosPorStatus(StatusPedido status);
        List<Pedido> ObterPedidosFinalizados();
        bool PedidoExiste(int id);
    }
}