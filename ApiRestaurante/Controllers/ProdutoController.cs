using System;
using ApiRestaurante.Domain.Models;
using ApiRestaurante.Domain.Models.Exceptions;
using ApiRestaurante.Services.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiRestaurante.Controllers
{
    [Authorize]
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
            return Ok(produtos);
        }

        [HttpGet("restaurante/produto/{id}")]
        public IActionResult ObterProdutoPorId([FromRoute] int id)
        {
            try
            {
                var produto = _produtoService.ObterProdutoPorId(id);
                if (produto == null)
                {
                    return NotFound();
                }
                return Ok(produto);
            }
            catch (Exception)
            {
                return StatusCode(500, "Erro interno do servidor.");
            }
        }

        [HttpPost("restaurante/produto")]
        public IActionResult CriarProduto([FromBody] Produto produto)
        {
            try
            {
                _produtoService.CriarProduto(produto);
                return StatusCode(201);
            }
            catch (ValidacaoException ex)
            {
                return StatusCode(400, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.ToString());
            }
        }

        [HttpPut("restaurante/produto/{id}")]
        public IActionResult AtualizarProduto([FromRoute] int id, [FromBody] Produto produto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != produto.IdProduto)
            {
                return BadRequest("O ID do produto na rota não corresponde ao ID no corpo da solicitação.");
            }

            try
            {
                _produtoService.AtualizarProduto(produto);
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, "Erro interno do servidor.");
            }
        }

        [HttpDelete("restaurante/produto/{id}")]
        public IActionResult DeletarProduto([FromRoute] int id)
        {
            try
            {
                _produtoService.DeletarProduto(id);
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, "Erro interno do servidor.");
            }
        }
    }
}