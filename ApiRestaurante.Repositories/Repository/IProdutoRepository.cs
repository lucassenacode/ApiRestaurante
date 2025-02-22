using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiRestaurante.Domain.Models;

namespace ApiRestaurante.Repositories.Repository
{
    public interface IProdutoRepository
    {
        List<Produto> ListarProdutos();
    }
}