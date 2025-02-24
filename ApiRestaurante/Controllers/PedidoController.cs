using System;
using System.Collections.Generic;
using ApiRestaurante.Domain.Models;
using ApiRestaurante.Domain.Models.Enuns;
using ApiRestaurante.Services.Service;
using Microsoft.AspNetCore.Mvc;

namespace ApiRestaurante.Controllers
{
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

        [HttpGet("restaurante/pedido/{id}")] // Corrigido o nome da rota
        public IActionResult PedidoPorId([FromRoute] int id)
        {
            try
            {
                var pedido = _pedidoService.ObterPedidoPorId(id); // Corrigido o nome da variável
                if (pedido == null)
                {
                    return NotFound();
                }
                return Ok(pedido); // Corrigido o tipo de retorno
            }
            catch (Exception ex)
            {
                // Logar a exceção
                return StatusCode(500, "Erro interno do servidor.");
            }
        }

        [HttpPost("restaurante/pedido")]
        public IActionResult CriarPedido([FromBody] Pedido pedido)
        {
            int pedidoId = _pedidoService.CriarPedido(pedido);
            return StatusCode(201, new { PedidoId = pedidoId });
        }

        [HttpDelete("restaurante/peodido/{id}")]
        public IActionResult DeletarPedido([FromRoute] int id)
        {
            try
            {
                _pedidoService.DeletarPedido(id);
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
        [HttpPut("restaurante/pedido/{id}/status")]
        public IActionResult AtualizarStatusPedido([FromRoute] int id, [FromBody] StatusPedido novoStatus)
        {
            try
            {
                _pedidoService.AtualizarStatusPedido(id, novoStatus);
                return NoContent();
            }
            catch (Exception ex)
            {
                // Logar a exceção
                return StatusCode(500, "Erro interno do servidor.");
            }
        }
        [HttpGet("restaurante/pedidos/status/{status}")]
        public IActionResult ObterPedidosPorStatus([FromRoute] StatusPedido status)
        {
            try
            {
                var pedidos = _pedidoService.ObterPedidosPorStatus(status);
                return Ok(pedidos);
            }
            catch (Exception ex)
            {
                // Logar a exceção
                return StatusCode(500, "Erro interno do servidor.");
            }
        }
        [HttpGet("restaurante/pedidos/setor/{setor}")]
        public IActionResult ObterPedidosPorSetor([FromRoute] string setor)
        {
            try
            {
                var pedidos = _pedidoService.ObterPedidosPorSetor(setor);
                return Ok(pedidos);
            }
            catch (Exception ex)
            {
                // Logar a exceção
                return StatusCode(500, "Erro interno do servidor.");
            }
        }

    }
}