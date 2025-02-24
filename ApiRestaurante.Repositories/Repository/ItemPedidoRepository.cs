using ApiRestaurante.Domain.Models;
using ApiRestaurante.Domain.Models.Enuns;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace ApiRestaurante.Repositories.Repository
{
    public class ItemPedidoRepository : Contexto, IItemPedidoRepository
    {
        public ItemPedidoRepository(IConfiguration configuration) : base(configuration)
        {
        }
        public void InserirItemPedido(ItemPedido item)
        {
            string comandosql = @"INSERT INTO ItemPedido (IdPedido, IdProduto, Quantidade)
                               VALUES (@IdPedido, @IdProduto, @Quantidade)";

            using (var cmd = new MySqlCommand(comandosql, _conn))
            {
                cmd.Parameters.AddWithValue("@IdPedido", item.IdPedido);
                cmd.Parameters.AddWithValue("@IdProduto", item.IdProduto);
                cmd.Parameters.AddWithValue("@Quantidade", item.Quantidade);
                cmd.ExecuteNonQuery();
            }
        }

        public List<ItemPedido> ListarItensPorPedido(int pedidoId)
        {
            string comandosql = @"SELECT ip.IdItemPedido, ip.IdPedido, ip.IdProduto, ip.Quantidade,
                                     p.NomeProduto, p.Preco, p.TipoProduto
                              FROM ItemPedido ip
                              JOIN Produto p ON ip.IdProduto = p.IdProduto
                              WHERE ip.IdPedido = @IdPedido";

            using (var cmd = new MySqlCommand(comandosql, _conn))
            {
                cmd.Parameters.AddWithValue("@IdPedido", pedidoId);

                using (var rdr = cmd.ExecuteReader())
                {
                    var itens = new List<ItemPedido>();
                    while (rdr.Read())
                    {
                        itens.Add(new ItemPedido
                        {
                            IdItemPedido = Convert.ToInt32(rdr["IdItemPedido"]),
                            IdPedido = Convert.ToInt32(rdr["IdPedido"]),
                            IdProduto = Convert.ToInt32(rdr["IdProduto"]),
                            Quantidade = Convert.ToInt32(rdr["Quantidade"]),
                            Produto = new Produto
                            {
                                IdProduto = Convert.ToInt32(rdr["IdProduto"]),
                                NomeProduto = rdr["NomeProduto"].ToString(),
                                Preco = Convert.ToDecimal(rdr["Preco"]),
                                TipoProduto = (TipoProduto)Enum.Parse(typeof(TipoProduto), rdr["TipoProduto"].ToString())
                            }
                        });
                    }
                    return itens;
                }
            }
        }

        public List<ItemPedido> ListarItensPedidoPorPedidoId(int pedidoId)
        {
            string comandosql = @"SELECT ip.IdItemPedido, ip.IdPedido, ip.IdProduto, ip.Quantidade,
                                   p.NomeProduto, p.Preco, p.TipoProduto
                            FROM ItemPedido ip
                            JOIN Produto p ON ip.IdProduto = p.IdProduto
                            WHERE ip.IdPedido = @IdPedido";

            using (var cmd = new MySqlCommand(comandosql, _conn))
            {
                cmd.Parameters.AddWithValue("@IdPedido", pedidoId);

                using (var rdr = cmd.ExecuteReader())
                {
                    var itens = new List<ItemPedido>();
                    while (rdr.Read())
                    {
                        itens.Add(new ItemPedido
                        {
                            IdItemPedido = Convert.ToInt32(rdr["IdItemPedido"]),
                            IdPedido = Convert.ToInt32(rdr["IdPedido"]),
                            IdProduto = Convert.ToInt32(rdr["IdProduto"]),
                            Quantidade = Convert.ToInt32(rdr["Quantidade"]),
                            Produto = new Produto
                            {
                                IdProduto = Convert.ToInt32(rdr["IdProduto"]),
                                NomeProduto = rdr["NomeProduto"].ToString(),
                                Preco = Convert.ToDecimal(rdr["Preco"]),
                                TipoProduto = (TipoProduto)Enum.Parse(typeof(TipoProduto), rdr["TipoProduto"].ToString())
                            }
                        });
                    }
                    return itens;
                }
            }
        }

        public void AtualizarItemPedido(ItemPedido item)
        {
            string comandosql = @"UPDATE ItemPedido SET IdPedido = @IdPedido, IdProduto = @IdProduto, Quantidade = @Quantidade WHERE IdItemPedido = @IdItemPedido";

            using (var cmd = new MySqlCommand(comandosql, _conn))
            {
                cmd.Parameters.AddWithValue("@IdItemPedido", item.IdItemPedido);
                cmd.Parameters.AddWithValue("@IdPedido", item.IdPedido);
                cmd.Parameters.AddWithValue("@IdProduto", item.IdProduto);
                cmd.Parameters.AddWithValue("@Quantidade", item.Quantidade);
                cmd.ExecuteNonQuery();
            }
        }

        public void RemoverItemPedido(int id)
        {
            string comandosql = @"DELETE FROM ItemPedido WHERE IdItemPedido = @IdItemPedido";

            using (var cmd = new MySqlCommand(comandosql, _conn))
            {
                cmd.Parameters.AddWithValue("@IdItemPedido", id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}