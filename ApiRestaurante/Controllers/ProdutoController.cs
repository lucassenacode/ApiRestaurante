using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiRestaurante.Domain.Models;
using ApiRestaurante.Services;
using ApiRestaurante.Services.Service;
using Microsoft.AspNetCore.Mvc;

namespace ApiRestaurante.Controllers
{
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoService _produtoService;

        public ProdutoController(IProdutoService produtoService)
        {
            _produtoService = produtoService;
        }

        [HttpGet("restaurante/produto")]
        public IActionResult ListarProdutos()
        {
            var produtos = _produtoService.ListarProdutos();
            return StatusCode(200, produtos);
        }

        [HttpPost("restaurante/produto")]
        public IActionResult InserirProduto([FromBody] Produto produto)
        {
            _produtoService.InserirProduto(produto);
            return StatusCode(201);
        }

        [HttpPut("restaurante/produto/")]
        public IActionResult AtualizarProduto([FromBody] Produto produto)
        {
            _produtoService.AtualizarProduto(produto);
            return StatusCode(201);
        }

        [HttpDelete("restaurante/produto/")]
        public IActionResult DeletarProduto(int idProdduto)
        {
            _produtoService.DeletarProduto(idProdduto);
            return StatusCode(200);
        }
    }

}