using System;
using System.Collections.Generic;
using System.Linq;
using ApiRestaurante.Domain.Models;
using ApiRestaurante.Domain.Models.Enuns;
using MySql.Data.MySqlClient;

namespace ApiRestaurante.Repositories.Repository
{
    public class PedidoRepository : Contexto, IPedidoRepository
    {
        public int CriarPedido(Pedido pedido)
        {
            string comandosql = @"INSERT INTO Pedido (NomeCliente, NumeroMesa, Status) 
                          VALUES (@NomeCliente, @NumeroMesa, @Status);
                          SELECT LAST_INSERT_ID();"; // Obtém o ID do pedido inserido

            using (var cmd = new MySqlCommand(comandosql, _conn))
            {
                cmd.Parameters.AddWithValue("@NomeCliente", pedido.NomeCliente);
                cmd.Parameters.AddWithValue("@NumeroMesa", pedido.NumeroMesa);
                cmd.Parameters.AddWithValue("@Status", pedido.Status.ToString()); // Converte o enum para string

                return Convert.ToInt32(cmd.ExecuteScalar()); // Executa a consulta e retorna o ID
            }
        }
        public List<Pedido> ListarPedidos()
        {
            string comandosql = @"SELECT p.IdPedido, p.NomeCliente, p.NumeroMesa, p.Status,
                                 i.IdItemPedido, i.IdProduto, i.Quantidade, 
                                 pr.NomeProduto, pr.Preco, pr.TipoProduto
                          FROM Pedido p
                          JOIN ItemPedido i ON p.IdPedido = i.IdPedido
                          JOIN Produto pr ON i.IdProduto = pr.IdProduto";

            using (var cmd = new MySqlCommand(comandosql, _conn))
            {
                using (var rdr = cmd.ExecuteReader())
                {
                    var pedidos = new Dictionary<int, Pedido>();
                    while (rdr.Read())
                    {
                        int pedidoId = Convert.ToInt32(rdr["IdPedido"]);
                        if (!pedidos.ContainsKey(pedidoId))
                        {
                            pedidos[pedidoId] = new Pedido
                            {
                                IdPedido = pedidoId,
                                NomeCliente = rdr["NomeCliente"].ToString(),
                                NumeroMesa = Convert.ToInt32(rdr["NumeroMesa"]),
                                Status = (StatusPedido)Enum.Parse(typeof(StatusPedido), rdr["Status"].ToString()),
                                CriadoEm = DateTime.Now
                            };
                        }

                        var itemPedido = new ItemPedido
                        {
                            IdItemPedido = Convert.ToInt32(rdr["IdItemPedido"]), // Corrigido
                            IdPedido = pedidoId, // Corrigido
                            IdProduto = Convert.ToInt32(rdr["IdProduto"]), // Corrigido
                            Produto = new Produto
                            {
                                IdProduto = Convert.ToInt32(rdr["IdProduto"]),
                                NomeProduto = rdr["NomeProduto"].ToString(),
                                Preco = Convert.ToDecimal(rdr["Preco"]),
                                TipoProduto = (TipoProduto)Enum.Parse(typeof(TipoProduto), rdr["TipoProduto"].ToString())
                            },
                            Quantidade = Convert.ToInt32(rdr["Quantidade"])
                        };

                        pedidos[pedidoId].Itens.Add(itemPedido);
                    }
                    return pedidos.Values.ToList();
                }
            }
        }
    }
}