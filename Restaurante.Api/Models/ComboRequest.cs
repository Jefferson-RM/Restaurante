public class CrearComboRequest
{
    public string Nombre { get; set; } = string.Empty;
    public decimal PrecioCombo { get; set; }
    public List<int> PlatoIds { get; set; } = new List<int>();
}