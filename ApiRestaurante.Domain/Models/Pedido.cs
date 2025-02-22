using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ApiRestaurante.Domain.Models.Enuns;

namespace ApiRestaurante.Domain.Models
{
    public class Pedido
    {
        public Pedido()
        {
            Itens = new List<ItemPedido>();
        }

        [JsonPropertyName("idPedido")]
        public int IdPedido { get; set; }

        [JsonPropertyName("nomeCliente")]
        [Required(ErrorMessage = "O nome do cliente é obrigatório.")]
        public string NomeCliente { get; set; }

        [JsonPropertyName("numeroMesa")]
        [Required(ErrorMessage = "O número da mesa é obrigatório.")]
        public int NumeroMesa { get; set; }

        [JsonPropertyName("itens")]
        public List<ItemPedido> Itens { get; set; } = new List<ItemPedido>();

        [JsonPropertyName("status")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public StatusPedido Status { get; set; } = StatusPedido.Novo;

        [JsonPropertyName("criadoEm")]
        public DateTime CriadoEm { get; set; } = DateTime.Now;

        public List<ItemPedido> ItensCopa => Itens.Where(i => i.Produto.TipoProduto == TipoProduto.Bebida).ToList();
        public List<ItemPedido> ItensCozinha => Itens.Where(i => i.Produto.TipoProduto == TipoProduto.Comida).ToList();


    }
}