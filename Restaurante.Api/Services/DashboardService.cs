using Microsoft.EntityFrameworkCore;

public class DashboardService
{
    private readonly RestauranteDbContext _db;

    public DashboardService(RestauranteDbContext db)
    {
        _db = db;
    }

    public void MostrarResumenVentas()
    {
        List<ItemPedido> todosLosItems = _db.ItemsPedido.ToList();

        var grupos = todosLosItems.GroupBy(item => item.TipoDeVenta);

        foreach (var grupo in grupos)
        {
            decimal totalDelGrupo = 0;
            foreach (ItemPedido item in grupo)
            {
                totalDelGrupo = totalDelGrupo + (item.PrecioUnitarioAplicado * item.Cantidad);
            }

            Console.WriteLine($"{grupo.Key}: RD${totalDelGrupo} ({grupo.Count()} ítems)");
        }
    }
}