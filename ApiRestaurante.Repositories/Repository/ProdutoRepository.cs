
using ApiRestaurante.Domain.Models;
using ApiRestaurante.Domain.Models.Enuns;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Cms;

namespace ApiRestaurante.Repositories.Repository
{
    public class ProdutoRepository : Contexto, IProdutoRepository
    {
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
                            TipoProduto = (TipoProduto)Enum.Parse(typeof(TipoProduto), Convert.ToString(rdr["TipoProduto"])) // Análise direta
                        };

                        produtos.Add(produto);
                    }

                    return produtos;
                }
            }
        }
    }
}

