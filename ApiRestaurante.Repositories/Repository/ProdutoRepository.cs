using System;
using System.Collections.Generic;
using System.Linq;
using ApiRestaurante.Domain.Models;
using ApiRestaurante.Domain.Models.Enuns;
using Microsoft.Extensions.Configuration; // Adicione esta linha
using MySql.Data.MySqlClient;

namespace ApiRestaurante.Repositories.Repository
{
    public class ProdutoRepository : Contexto, IProdutoRepository
    {
        public ProdutoRepository(IConfiguration configuration) : base(configuration)
        {
        }
        public List<Produto> ListarProdutos()
        {
            string comandosql = @"SELECT IdProduto, NomeProduto, Preco, TipoProduto FROM Produto";

            using (var cmd = new MySqlCommand(comandosql, _conn))
            {
                using (var rdr = cmd.ExecuteReader())
                {
                    var produtos = new List<Produto>();

                    while (rdr.Read())
                    {
                        var produto = new Produto
                        {
                            IdProduto = Convert.ToInt32(rdr["IdProduto"]),
                            NomeProduto = Convert.ToString(rdr["NomeProduto"]),
                            Preco = Convert.ToDecimal(rdr["Preco"]),
                            TipoProduto = (TipoProduto)Enum.Parse(typeof(TipoProduto), Convert.ToString(rdr["TipoProduto"]))
                        };

                        produtos.Add(produto);
                    }
                    return produtos;
                }
            }
        }

        public Produto ObterProdutoPorId(int idProduto)
        {
            string comandosql = @"SELECT IdProduto, NomeProduto, Preco, TipoProduto 
                            FROM Produto 
                            WHERE IdProduto = @IdProduto";

            using (var cmd = new MySqlCommand(comandosql, _conn))
            {
                cmd.Parameters.AddWithValue("@IdProduto", idProduto);
                using (var rdr = cmd.ExecuteReader())
                {
                    if (rdr.Read())
                    {
                        return new Produto
                        {
                            IdProduto = Convert.ToInt32(rdr["IdProduto"]),
                            NomeProduto = Convert.ToString(rdr["NomeProduto"]),
                            Preco = Convert.ToDecimal(rdr["Preco"]),
                            TipoProduto = (TipoProduto)Enum.Parse(typeof(TipoProduto), Convert.ToString(rdr["TipoProduto"]))
                        };
                    }
                    return null;
                }
            }
        }

        public void CriarProduto(Produto produto)
        {

            string comandosql = @"INSERT INTO Produto(NomeProduto, Preco, 
            TipoProduto) VALUES (@NomeProduto, @Preco, @TipoProduto);";

            using (var cmd = new MySqlCommand(comandosql, _conn))
            {
                cmd.Parameters.AddWithValue("@NomeProduto", produto.NomeProduto);
                cmd.Parameters.AddWithValue("@Preco", produto.Preco);
                cmd.Parameters.AddWithValue("@TipoProduto", produto.TipoProduto);
                cmd.ExecuteNonQuery();

            }
        }

        public void AtualizarProduto(Produto produto)
        {
            string comandosql = @"UPDATE Produto SET
                                    NomeProduto = @NomeProduto,
                                    Preco = @Preco,
                                    TipoProduto = @TipoProduto
                                WHERE IdProduto = @IdProduto";

            using (var cmd = new MySqlCommand(comandosql, _conn))
            {
                cmd.Parameters.AddWithValue("@IdProduto", produto.IdProduto);
                cmd.Parameters.AddWithValue("@NomeProduto", produto.NomeProduto);
                cmd.Parameters.AddWithValue("@Preco", produto.Preco);
                cmd.Parameters.AddWithValue("@TipoProduto", produto.TipoProduto.ToString());
                cmd.ExecuteNonQuery();

            }
        }

        public void DeletarProduto(int idProduto)
        {
            string comandosql = @"DELETE FROM Produto WHERE IdProduto = @IdProduto";

            using (var cmd = new MySqlCommand(comandosql, _conn))
            {
                cmd.Parameters.AddWithValue("@IdProduto", idProduto);

                if (cmd.ExecuteNonQuery() == 0)
                {
                    throw new InvalidOperationException($"Nenhum produto afetado para esse {idProduto}.");
                }
            }
        }

        public bool ProdutoExiste(int idProduto)
        {

            string comandosql = @"SELECT COUNT(*) FROM Produto WHERE IdProduto = @IDProduto";

            using (var cmd = new MySqlCommand(comandosql, _conn))
            {
                cmd.Parameters.AddWithValue("@IdProduto", idProduto);
                return Convert.ToBoolean(cmd.ExecuteScalar());
            }
        }
    }
}
