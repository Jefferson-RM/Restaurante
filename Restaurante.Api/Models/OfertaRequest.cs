public class CrearOfertaRequest
{
    public DayOfWeek DiaSemana { get; set; }
    public decimal PrecioOferta { get; set; }
    public List<int> PlatoIds { get; set; } = new List<int>();
}