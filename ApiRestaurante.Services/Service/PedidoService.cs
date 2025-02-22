using System.Collections.Generic;
using ApiRestaurante.Domain.Models;
using ApiRestaurante.Repositories.Repository;

namespace ApiRestaurante.Services.Service
{
    public class PedidoService : IPedidoService
    {
        private readonly IPedidoRepository _repositorio;
        private readonly IItemPedidoRepository _itemPedidoRepository;

        public PedidoService(IPedidoRepository repositorio, IItemPedidoRepository itemPedidoRepository)
        {
            _repositorio = repositorio;
            _itemPedidoRepository = itemPedidoRepository;
        }

        public int CriarPedido(Pedido pedido)
        {
            ((Contexto)_repositorio).AbrirConexao();
            try
            {
                int idPedido = _repositorio.CriarPedido(pedido);

                foreach (var item in pedido.Itens)
                {
                    item.IdPedido = idPedido;
                    _itemPedidoRepository.InserirItemPedido(item);
                }

                return idPedido;
            }
            finally
            {
                ((Contexto)_repositorio).FecharConexao();
            }
        }

        public List<Pedido> ListarPedidos()
        {
            ((Contexto)_repositorio).AbrirConexao();
            try
            {
                return _repositorio.ListarPedidos();
            }
            finally
            {
                ((Contexto)_repositorio).FecharConexao();
            }
        }
    }
}
