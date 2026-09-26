public class Plato
{
     public int Id { get; set; } 
      public string Nombre { get; set; } = string.Empty;
    public decimal Precio { get; set; }
    public CategoriaPlato Categoria { get; set; }
}