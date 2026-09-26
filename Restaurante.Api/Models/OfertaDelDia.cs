public class OfertaDelDia
{
     public int Id { get; set; } 
     public DayOfWeek DiaSemana { get; set; }
    public List<Plato> Platos { get; set; } = new List<Plato>();
    public decimal PrecioOferta { get; set; }
}