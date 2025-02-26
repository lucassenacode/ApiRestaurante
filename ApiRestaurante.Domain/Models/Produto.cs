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
        [Required(ErrorMessage = "O nome do produto é obrigatório.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome do produto deve ter entre 3 e 100 caracteres.")]
        public string NomeProduto { get; set; }

        [JsonPropertyName("Preco")]
        [Required(ErrorMessage = "O preço do produto é obrigatório.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O preço do produto deve ser maior que zero.")]
        public decimal Preco { get; set; }

        [JsonPropertyName("TipoProduto")]
        [Required(ErrorMessage = "O tipo do produto é obrigatório.")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public TipoProduto TipoProduto { get; set; }
    }
}
