using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class DashboardController : ControllerBase
{
    private readonly RestauranteDbContext _db;

    public DashboardController(RestauranteDbContext db)
    {
        _db = db;
    }

    // GET api/dashboard/ventas -> resumen de ventas por tipo (Normal/Oferta/Combo)
    [HttpGet("ventas")]
    public IActionResult ObtenerResumenVentas()
    {
        List<ItemPedido> todosLosItems = _db.ItemsPedido.ToList();

        var resumen = todosLosItems
            .GroupBy(item => item.TipoDeVenta)
            .Select(grupo => new
            {
                Tipo = grupo.Key.ToString(),
                Total = grupo.Sum(item => item.PrecioUnitarioAplicado * item.Cantidad),
                CantidadItems = grupo.Count()
            })
            .ToList();

        return Ok(resumen);
    }
}