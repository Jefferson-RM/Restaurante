public class Pedido
{
    public int Id { get; set; }  
     public DateTime Fecha { get; set; } = DateTime.Now;
    public List<ItemPedido> Items { get; set; } = new List<ItemPedido>();

    public decimal CalcularSubtotal()
    {
        decimal subtotal = 0;
        foreach (ItemPedido item in Items)
        {
            subtotal = subtotal + (item.PrecioUnitarioAplicado * item.Cantidad);
        }
        return subtotal;
    }

    public decimal CalcularItbis()
    {
        return CalcularSubtotal() * 0.18m;
    }

    public decimal CalcularPropinaLegal()
    {
        return CalcularSubtotal() * 0.10m;
    }

    public decimal CalcularTotal()
    {
        return CalcularSubtotal() + CalcularItbis() + CalcularPropinaLegal();
    }
}