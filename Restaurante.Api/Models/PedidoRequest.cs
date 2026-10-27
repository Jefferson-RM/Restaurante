public class CrearPedidoRequest
{
    public string NombreCliente { get; set; } = string.Empty;
    public string Telefono { get; set; } = string.Empty;
    public TipoPedido TipoPedido { get; set; }
    public List<ItemSolicitado> Items { get; set; } = new List<ItemSolicitado>();
}