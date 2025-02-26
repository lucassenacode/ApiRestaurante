using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ApiRestaurante.Domain.Models;

public class ItemPedido
{
    [JsonPropertyName("idItemPedido")]
    public int IdItemPedido { get; set; }

    [JsonPropertyName("idPedido")]
    [Required(ErrorMessage = "O ID do pedido é obrigatório.")]
    public int IdPedido { get; set; }

    [JsonIgnore]
    public virtual Pedido? Pedido { get; set; }

    [JsonPropertyName("idProduto")]
    [Required(ErrorMessage = "O ID do produto é obrigatório.")]
    [Range(1, int.MaxValue, ErrorMessage = "O ID do produto deve ser maior que zero.")]
    public int IdProduto { get; set; }

    [JsonPropertyName("produto")]
    [JsonIgnore]
    public virtual Produto? Produto { get; set; }

    [JsonPropertyName("quantidade")]
    [Required(ErrorMessage = "A quantidade é obrigatória.")]
    [Range(1, int.MaxValue, ErrorMessage = "A quantidade deve ser maior que zero.")]
    public int Quantidade { get; set; }
}