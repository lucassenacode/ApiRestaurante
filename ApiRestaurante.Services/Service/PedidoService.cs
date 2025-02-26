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
            ((Contexto)_itemPedidoRepository).AbrirConexao();
            try
            {

                ValidarPedido(pedido);


                foreach (var item in pedido.Itens)
                {
                    Console.WriteLine($"Buscando produto com IdProduto: {item.IdProduto}");
                    var produto = _produtoRepository.ObterProdutoPorId(item.IdProduto);
                    if (produto == null)
                    {
                        Console.WriteLine($"Produto não encontrado: {item.IdProduto}");
                        throw new InvalidOperationException($"Produto não encontrado: {item.IdProduto}");
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
            catch (InvalidOperationException ex)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
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

        public List<Pedido> ObterPedidosFinalizados()
        {
            ((Contexto)_repositorio).AbrirConexao();
            try
            {
                return _repositorio.ObterPedidosFinalizados();
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

        private void ValidarPedido(Pedido pedido)
        {
            if (pedido == null)
            {
                throw new InvalidOperationException("O JSON está mal formatado ou foi enviado vazio.");
            }

            if (string.IsNullOrWhiteSpace(pedido.NomeCliente))
            {
                throw new InvalidOperationException("O nome do cliente é obrigatório.");
            }

            if (pedido.NomeCliente.Trim().Length < 3 || pedido.NomeCliente.Trim().Length > 255)
            {
                throw new InvalidOperationException("O nome do cliente precisa ter entre 3 e 255 caracteres.");
            }

            if (pedido.NumeroMesa <= 0)
            {
                throw new InvalidOperationException("O número da mesa deve ser maior que zero.");
            }

            if (pedido.Itens == null || pedido.Itens.Count == 0)
            {
                throw new InvalidOperationException("O pedido deve conter pelo menos um item.");
            }

            foreach (var item in pedido.Itens)
            {
                if (item.IdProduto <= 0)
                {
                    throw new InvalidOperationException("O ID do produto é inválido.");
                }

                if (item.Quantidade <= 0)
                {
                    throw new InvalidOperationException("A quantidade de cada item deve ser maior que zero.");
                }
            }
        }
    }
}