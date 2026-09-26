public class ItemPedido
{
    public int Id { get; set; } 
    public Plato? Plato { get; set; }
    public Combo? Combo { get; set; }
    public int Cantidad { get; set; }
    public TipoDeVenta TipoDeVenta { get; set; }
    public decimal PrecioUnitarioAplicado { get; set; }
}

