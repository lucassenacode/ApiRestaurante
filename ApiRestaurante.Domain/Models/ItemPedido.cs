using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ApiRestaurante.Domain.Models;

public class ItemPedido
{
    [JsonPropertyName("idItemPedido")]
    public int IdItemPedido { get; set; }

    [JsonPropertyName("idPedido")]
    public int IdPedido { get; set; }

    [JsonIgnore]
    public virtual Pedido? Pedido { get; set; }

    [JsonPropertyName("idProduto")]
    public int IdProduto { get; set; }

    [JsonPropertyName("produto")]
    [JsonIgnore] // Adicione esta linha
    public virtual Produto? Produto { get; set; }

    [JsonPropertyName("quantidade")]
    [Required(ErrorMessage = "A quantidade é obrigatória.")]
    public int Quantidade { get; set; }
}