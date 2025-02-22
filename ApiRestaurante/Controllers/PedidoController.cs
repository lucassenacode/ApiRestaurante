using System.Collections.Generic;
using ApiRestaurante.Domain.Models;
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

        [HttpPost("restaurante/pedido")]
        public IActionResult CriarPedido([FromBody] Pedido pedido)
        {
            int pedidoId = _pedidoService.CriarPedido(pedido);
            return StatusCode(201, new { PedidoId = pedidoId });
        }

    }
}
