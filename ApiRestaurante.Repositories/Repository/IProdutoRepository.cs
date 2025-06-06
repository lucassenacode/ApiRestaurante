using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using ApiRestaurante.Domain.Models;

namespace ApiRestaurante.Repositories.Repository
{
    public interface IProdutoRepository
    {
        List<Produto> ListarProdutos();
        Produto ObterProdutoPorId(int idProduto);
        void CriarProduto(Produto produto);
        void AtualizarProduto(Produto produto);
        void DeletarProduto(int idProdduto);
        bool ProdutoExiste(int idProdduto);
    }

}