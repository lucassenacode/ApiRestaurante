using System;
using System.Collections.Generic;
using ApiRestaurante.Domain.Models;
using ApiRestaurante.Domain.Models.Enuns;
using ApiRestaurante.Domain.Models.Exceptions;
using ApiRestaurante.Services.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiRestaurante.Controllers
{
    [Authorize]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        private readonly IPedidoService _pedidoService;

        public PedidoController(IPedidoService pedidoService)
        {
            _pedidoService = pedidoService;
        }

        [HttpGet("restaurante/pedidos")]
        public IActionResult ListarPedidos()
        {
            var pedidos = _pedidoService.ListarPedidos();
            return Ok(pedidos);
        }

        [HttpGet("restaurante/pedido/{id}")]
        public IActionResult PedidoPorId([FromRoute] int id)
        {
            try
            {
                var pedido = _pedidoService.ObterPedidoPorId(id);
                if (pedido == null)
                {
                    return NotFound();
                }
                return Ok(pedido);
            }
            catch (Exception)
            {

                return StatusCode(500, "Erro interno do servidor.");
            }
        }

        [HttpPost("restaurante/pedido")]
        public IActionResult CriarPedido([FromBody] Pedido pedido)
        {
            try
            {
                int pedidoId = _pedidoService.CriarPedido(pedido);
                return StatusCode(201, new { PedidoId = pedidoId });
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

        [HttpPut("restaurante/pedido/{id}")]
        public IActionResult AtualizarPedido([FromRoute] int id, [FromBody] Pedido pedido)
        {
            if (id != pedido.IdPedido)
            {
                return BadRequest("O ID do pedido na rota não corresponde ao ID no corpo da solicitação.");
            }

            try
            {
                _pedidoService.AtualizarPedido(pedido);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return StatusCode(400, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.ToString());
            }
        }

        [HttpDelete("restaurante/peodido/{id}")]
        public IActionResult DeletarPedido([FromRoute] int id)
        {
            try
            {
                _pedidoService.DeletarPedido(id);
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, "Erro interno do servidor.");
            }
        }

        [HttpPut("restaurante/pedido/{id}/status")]
        public IActionResult AtualizarStatusPedido([FromRoute] int id, [FromBody] StatusPedido novoStatus)
        {
            try
            {
                _pedidoService.AtualizarStatusPedido(id, novoStatus);
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, "Erro interno do servidor.");
            }
        }


        [HttpGet("restaurante/pedidos/copa")]
        public IActionResult ListarPedidosCopa()
        {
            try
            {
                var pedidos = _pedidoService.ListarPedidos();
                var pedidosCopa = pedidos.Select(p => new
                {
                    p.IdPedido,
                    p.NomeCliente,
                    p.NumeroMesa,
                    p.Status,
                    p.CriadoEm,
                    Itens = p.ItensCopa
                }).Where(p => p.Itens.Any()).ToList();

                return Ok(pedidosCopa);
            }
            catch (Exception)
            {

                return StatusCode(500, "Erro interno do servidor.");
            }
        }

        [HttpGet("restaurante/pedidos/cozinha")]
        public IActionResult ListarPedidosCozinha()
        {
            try
            {
                var pedidos = _pedidoService.ListarPedidos();
                var pedidosCozinha = pedidos.Select(p => new
                {
                    p.IdPedido,
                    p.NomeCliente,
                    p.NumeroMesa,
                    p.Status,
                    p.CriadoEm,
                    Itens = p.ItensCozinha
                }).Where(p => p.Itens.Any()).ToList();

                return Ok(pedidosCozinha);
            }
            catch (Exception)
            {

                return StatusCode(500, "Erro interno do servidor.");
            }
        }

        [HttpGet("restaurante/pedidos/historico")]
        public IActionResult ListarPedidosFinalizados()
        {
            try
            {
                var pedidos = _pedidoService.ObterPedidosFinalizados();
                return Ok(pedidos);
            }
            catch (Exception)
            {
                return StatusCode(500, "Erro interno do servidor.");
            }
        }
    }
}