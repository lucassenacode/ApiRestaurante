using System;
using System.Collections.Generic;
using ApiRestaurante.Domain.Models;
using ApiRestaurante.Domain.Models.Exceptions;
using ApiRestaurante.Services.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiRestaurante.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    public class ItemPedidoController : ControllerBase
    {
        private readonly IItemPedidoService _itemPedidoService;

        public ItemPedidoController(IItemPedidoService itemPedidoService)
        {
            _itemPedidoService = itemPedidoService;
        }

        [HttpGet("restaurante/itemPedido/{pedidoId}")]
        public IActionResult ListarItensPedidoPorPedidoId([FromRoute] int pedidoId)
        {
            if (pedidoId <= 0)
            {
                return BadRequest("O ID do pedido deve ser maior que zero.");
            }

            try
            {
                var itens = _itemPedidoService.ListarItensPedidoPorPedidoId(pedidoId);
                if (itens == null || itens.Count == 0)
                {
                    return NotFound($"Nenhum item encontrado para o pedido com ID {pedidoId}.");
                }
                return Ok(itens);
            }
            catch (Exception)
            {

                return StatusCode(500, "Erro interno do servidor.");
            }
        }

        [HttpPost("restaurante/itemPedido")]
        public IActionResult AdicionarItemAoPedido([FromBody] ItemPedido item)
        {
            try
            {
                _itemPedidoService.AdicionarItemAoPedido(item);
                return StatusCode(201);
            }
            catch (ValidacaoException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Erro interno do servidor.");
            }
        }

        [HttpPut("restaurante/itemPedido/{idItemPedido}")]
        public IActionResult AtualizarItemPedido([FromRoute] int idItemPedido, [FromBody] ItemPedido item)
        {
            if (idItemPedido != item.IdItemPedido)
            {
                return BadRequest("O ID do item do pedido na rota não corresponde ao ID no corpo da requisição.");
            }
            try
            {
                _itemPedidoService.AtualizarItemPedido(item);
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

        [HttpDelete("restaurante/itemPedido/{idItemPedido}")]
        public IActionResult RemoverItemPedido([FromRoute] int idItemPedido)
        {
            try
            {
                _itemPedidoService.RemoverItemPedido(idItemPedido);
                return NoContent();
            }
            catch (Exception)
            {

                return StatusCode(500, "Erro interno do servidor.");
            }
        }
    }
}