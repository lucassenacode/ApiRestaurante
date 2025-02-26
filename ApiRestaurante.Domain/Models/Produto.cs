using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using ApiRestaurante.Domain.Models.Enuns;

namespace ApiRestaurante.Domain.Models
{
    public class Produto
    {
        [JsonPropertyName("IdProduto")]
        public int IdProduto { get; set; }

        [JsonPropertyName("NomeProduto")]
        [Required]
        public string NomeProduto { get; set; }

        [JsonPropertyName("Preco")]
        [Required]
        public decimal Preco { get; set; }

        [JsonPropertyName("TipoProduto")]
        [Required]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public TipoProduto TipoProduto { get; set; }
    }
}