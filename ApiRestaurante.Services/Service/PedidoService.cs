using System;
using System.Collections.Generic;
using ApiRestaurante.Domain.Models;
using ApiRestaurante.Repositories.Repository;

namespace ApiRestaurante.Services.Service
{
    public class PedidoService : IPedidoService
    {
        private readonly IPedidoRepository _repositorio;
        private readonly IItemPedidoRepository _itemPedidoRepository;
        private readonly IProdutoRepository _produtoRepository;

        public PedidoService(
            IPedidoRepository repositorio,
            IItemPedidoRepository itemPedidoRepository,
            IProdutoRepository produtoRepository)
        {
            _repositorio = repositorio;
            _itemPedidoRepository = itemPedidoRepository;
            _produtoRepository = produtoRepository;
        }

        public int CriarPedido(Pedido pedido)
        {
            ((Contexto)_repositorio).AbrirConexao();
            ((Contexto)_produtoRepository).AbrirConexao();
            ((Contexto)_itemPedidoRepository).AbrirConexao(); // Adicionar esta linha
            try
            {
                foreach (var item in pedido.Itens)
                {
                    var produto = _produtoRepository.ObterProdutoPorId(item.IdProduto);
                    if (produto == null)
                    {
                        throw new Exception($"Produto não encontrado: {item.IdProduto}");
                    }
                    item.Produto = produto;
                }

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
                ((Contexto)_produtoRepository).FecharConexao();
                ((Contexto)_itemPedidoRepository).FecharConexao(); // Adicionar esta linha
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