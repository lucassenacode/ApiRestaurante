using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiRestaurante.Domain.Models;
using ApiRestaurante.Services.Service;
using Microsoft.AspNetCore.Mvc;

namespace ApiRestaurante.Controllers
{
    public class ItempedidoController
    {
        [ApiController]
        public class ItemPedidoController : ControllerBase
        {
            private readonly IItemPedidoService _itemPedidoService;

            public ItemPedidoController(IItemPedidoService itemPedidoService)
            {
                _itemPedidoService = itemPedidoService;
            }

            [HttpPost("restaurante/pedido/item")]
            public IActionResult AdicionarItemAoPedido([FromBody] ItemPedido item)
            {
                _itemPedidoService.AdicionarItemAoPedido(item);
                return StatusCode(201);
            }

            [HttpGet("restaurante/pedido/{pedidoId}/itens")]
            public IActionResult ListarItensPorPedido(int pedidoId)
            {
                var itens = _itemPedidoService.ListarItensPorPedido(pedidoId);
                return Ok(itens);
            }
        }
    }
}
