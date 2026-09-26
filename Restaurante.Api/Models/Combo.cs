public class Combo
{
     public int Id { get; set; } 
      public string Nombre { get; set; } = string.Empty;
    public decimal PrecioCombo { get; set; }
    public List<Plato> Platos { get; set; } = new List<Plato>();

    public decimal CalcularSumaIndividual()
    {
        decimal total = 0;
        foreach (Plato p in Platos)
        {
            total = total + p.Precio;
        }
        return total;
    }
}
