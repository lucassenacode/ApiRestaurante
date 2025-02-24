using System;
using ApiRestaurante.Domain.Models;
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
            catch (Exception ex)
            {
                // Logar a exceção
                return StatusCode(500, "Erro interno do servidor.");
            }
        }

        [HttpPost("restaurante/produto")]
        public IActionResult CriarProduto([FromBody] Produto produto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _produtoService.CriarProduto(produto);
                return CreatedAtAction(nameof(ObterProdutoPorId), new { id = produto.IdProduto }, produto);
            }
            catch (Exception ex)
            {
                // Logar a exceção
                return StatusCode(500, "Erro interno do servidor.");
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
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                // Logar a exceção
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
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Erro interno do servidor.");
            }
        }
    }
}