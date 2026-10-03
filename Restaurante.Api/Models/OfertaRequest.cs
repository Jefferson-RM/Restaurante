public class CrearOfertaRequest
{
    public DateTime FechaInicio { get; set; }
    public DateTime FechaFin { get; set; }
    public decimal PrecioOferta { get; set; }
    public List<int> PlatoIds { get; set; } = new List<int>();
}