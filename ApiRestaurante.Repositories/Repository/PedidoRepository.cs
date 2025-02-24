using System;
using System.Collections.Generic;
using System.Linq;
using ApiRestaurante.Domain.Models;
using ApiRestaurante.Domain.Models.Enuns;
using Microsoft.Extensions.Configuration; // Adicione esta linha
using MySql.Data.MySqlClient;

namespace ApiRestaurante.Repositories.Repository
{
    public class PedidoRepository : Contexto, IPedidoRepository
    {
        public PedidoRepository(IConfiguration configuration) : base(configuration)
        {
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

        public Pedido ObterPedidoPorId(int id)
        {
            string comandosql = @"SELECT IdPedido, NomeCliente, NumeroMesa, Status, CriadoEm 
                           FROM Pedido 
                           WHERE IdPedido = @IdPedido";

            using (var cmd = new MySqlCommand(comandosql, _conn))
            {
                cmd.Parameters.AddWithValue("@IdPedido", id);
                using (var rdr = cmd.ExecuteReader())
                {
                    if (rdr.Read())
                    {
                        return new Pedido
                        {
                            IdPedido = Convert.ToInt32(rdr["IdPedido"]),
                            NomeCliente = Convert.ToString(rdr["NomeCliente"]),
                            NumeroMesa = Convert.ToInt32(rdr["NumeroMesa"]),
                            Status = (StatusPedido)Enum.Parse(typeof(StatusPedido), Convert.ToString(rdr["Status"])),
                            CriadoEm = Convert.ToDateTime(rdr["CriadoEm"])
                        };
                    }
                    return null; // Retorna null se o pedido não for encontrado
                }
            }
        }
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
        public void AtualizarPedido(Pedido pedido)
        {
            string comandosql = @"UPDATE Pedido 
                           SET NomeCliente = @NomeCliente, NumeroMesa = @NumeroMesa, Status = @Status 
                           WHERE IdPedido = @IdPedido";

            using (var cmd = new MySqlCommand(comandosql, _conn))
            {
                cmd.Parameters.AddWithValue("@IdPedido", pedido.IdPedido);
                cmd.Parameters.AddWithValue("@NomeCliente", pedido.NomeCliente);
                cmd.Parameters.AddWithValue("@NumeroMesa", pedido.NumeroMesa);
                cmd.Parameters.AddWithValue("@Status", pedido.Status.ToString());
                cmd.ExecuteNonQuery();
            }
        }
        public void DeletarPedido(int id)
        {
            string comandosql = @"DELETE FROM Pedido WHERE IdPedido = @IdPedido";

            using (var cmd = new MySqlCommand(comandosql, _conn))
            {
                cmd.Parameters.AddWithValue("@IdPedido", id);
                cmd.ExecuteNonQuery();
            }
        }
        public void AtualizarStatusPedido(int id, StatusPedido novoStatus)
        {
            string comandosql = @"UPDATE Pedido SET Status = @Status WHERE IdPedido = @IdPedido";

            using (var cmd = new MySqlCommand(comandosql, _conn))
            {
                cmd.Parameters.AddWithValue("@IdPedido", id);
                cmd.Parameters.AddWithValue("@Status", novoStatus.ToString());
                cmd.ExecuteNonQuery();
            }
        }
        public List<Pedido> ObterPedidosPorStatus(StatusPedido status)
        {
            string comandosql = @"SELECT IdPedido, NomeCliente, NumeroMesa, Status, CriadoEm 
                           FROM Pedido 
                           WHERE Status = @Status";

            using (var cmd = new MySqlCommand(comandosql, _conn))
            {
                cmd.Parameters.AddWithValue("@Status", status.ToString());
                using (var rdr = cmd.ExecuteReader())
                {
                    var pedidos = new List<Pedido>();
                    while (rdr.Read())
                    {
                        pedidos.Add(new Pedido
                        {
                            IdPedido = Convert.ToInt32(rdr["IdPedido"]),
                            NomeCliente = Convert.ToString(rdr["NomeCliente"]),
                            NumeroMesa = Convert.ToInt32(rdr["NumeroMesa"]),
                            Status = (StatusPedido)Enum.Parse(typeof(StatusPedido), Convert.ToString(rdr["Status"])),
                            CriadoEm = Convert.ToDateTime(rdr["CriadoEm"])
                        });
                    }
                    return pedidos;
                }
            }
        }
        public List<Pedido> ObterPedidosPorSetor(string setor)
        {
            string comandosql = @"SELECT p.IdPedido, p.NomeCliente, p.NumeroMesa, p.Status, p.CriadoEm 
                           FROM Pedido p
                           JOIN ItemPedido ip ON p.IdPedido = ip.IdPedido
                           JOIN Produto pr ON ip.IdProduto = pr.IdProduto
                           WHERE pr.TipoProduto = @Setor";

            using (var cmd = new MySqlCommand(comandosql, _conn))
            {
                cmd.Parameters.AddWithValue("@Setor", setor);
                using (var rdr = cmd.ExecuteReader())
                {
                    var pedidos = new List<Pedido>();
                    while (rdr.Read())
                    {
                        pedidos.Add(new Pedido
                        {
                            IdPedido = Convert.ToInt32(rdr["IdPedido"]),
                            NomeCliente = Convert.ToString(rdr["NomeCliente"]),
                            NumeroMesa = Convert.ToInt32(rdr["NumeroMesa"]),
                            Status = (StatusPedido)Enum.Parse(typeof(StatusPedido), Convert.ToString(rdr["Status"])),
                            CriadoEm = Convert.ToDateTime(rdr["CriadoEm"])
                        });
                    }
                    return pedidos;
                }
            }
        }
        public bool PedidoExiste(int id)
        {
            string comandosql = @"SELECT COUNT(*) FROM Pedido WHERE IdPedido = @IdPedido";

            using (var cmd = new MySqlCommand(comandosql, _conn))
            {
                cmd.Parameters.AddWithValue("@IdPedido", id);
                return Convert.ToBoolean(cmd.ExecuteScalar());
            }
        }
    }
}