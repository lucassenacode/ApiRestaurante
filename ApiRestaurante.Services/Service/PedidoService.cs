using System;
using System.Collections.Generic;
using ApiRestaurante.Domain.Models;
using ApiRestaurante.Domain.Models.Enuns;
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

        public Pedido ObterPedidoPorId(int idPedido)
        {
            try
            {
                ((Contexto)_repositorio).AbrirConexao();
                return _repositorio.ObterPedidoPorId(idPedido);
            }
            finally
            {
                ((Contexto)_repositorio).FecharConexao();
            }
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
                ((Contexto)_itemPedidoRepository).FecharConexao();
            }
        }

        public void AtualizarPedido(Pedido pedido)
        {

            try
            {
                ((Contexto)_repositorio).AbrirConexao();

                if (!_repositorio.PedidoExiste(pedido.IdPedido))
                {
                    throw new Exception($"Produto de ID: {pedido.IdPedido} não encontrado");
                }
                _repositorio.AtualizarPedido(pedido);
            }
            finally
            {
                ((Contexto)_repositorio).FecharConexao();
            }
        }
        public void DeletarPedido(int idPedido)
        {
            try
            {
                ((Contexto)_repositorio).AbrirConexao();
                if (!_repositorio.PedidoExiste(idPedido))
                {
                    throw new Exception($"Produto de ID: {idPedido} não encontrado");
                }
                _repositorio.DeletarPedido(idPedido);
            }
            finally
            {
                ((Contexto)_repositorio).FecharConexao();
            }
        }
        public void AtualizarStatusPedido(int id, StatusPedido novoStatus)
        {
            ((Contexto)_repositorio).AbrirConexao();
            try
            {
                _repositorio.AtualizarStatusPedido(id, novoStatus);
            }
            finally
            {
                ((Contexto)_repositorio).FecharConexao();
            }
        }
        public List<Pedido> ObterPedidosPorStatus(StatusPedido status)
        {
            ((Contexto)_repositorio).AbrirConexao();
            try
            {
                return _repositorio.ObterPedidosPorStatus(status);
            }
            finally
            {
                ((Contexto)_repositorio).FecharConexao();
            }
        }
        public List<Pedido> ObterPedidosPorSetor(string setor)
        {
            ((Contexto)_repositorio).AbrirConexao();
            try
            {
                return _repositorio.ObterPedidosPorSetor(setor);
            }
            finally
            {
                ((Contexto)_repositorio).FecharConexao();
            }
        }
        public bool PedidoExiste(int id)
        {
            ((Contexto)_repositorio).AbrirConexao();
            try
            {
                return _repositorio.PedidoExiste(id);
            }
            finally
            {
                ((Contexto)_repositorio).FecharConexao();
            }
        }
    }
}