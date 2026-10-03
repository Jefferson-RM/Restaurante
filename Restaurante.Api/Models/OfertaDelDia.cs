public class OfertaDelDia
{
    public int Id { get; set; }
    public DateTime FechaInicio { get; set; }
    public DateTime FechaFin { get; set; }
    public List<Plato> Platos { get; set; } = new List<Plato>();
    public decimal PrecioOferta { get; set; }
}