using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiRestaurante.Domain.Models;

namespace ApiRestaurante.Services.Service
{
    public interface IProdutoService
    {
        List<Produto> ListarProdutos();
        void InserirProduto(Produto produto);
        void AtualizarProduto(Produto produto);

        void DeletarProduto(int idProdduto);

    }
}