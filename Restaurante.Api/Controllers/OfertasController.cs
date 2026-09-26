using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class OfertasController : ControllerBase
{
    private readonly RestauranteDbContext _db;
    private readonly MenuService _menuService;

    public OfertasController(RestauranteDbContext db, MenuService menuService)
    {
        _db = db;
        _menuService = menuService;
    }

    // GET api/ofertas -> lista todas las ofertas
    [HttpGet]
    public IActionResult ObtenerOfertas()
    {
        List<OfertaDelDia> ofertas = _db.Ofertas.Include(o => o.Platos).ToList();
        return Ok(ofertas);
    }

    // GET api/ofertas/hoy -> las ofertas activas hoy
    [HttpGet("hoy")]
    public IActionResult ObtenerOfertasDeHoy()
    {
        List<OfertaDelDia> ofertas = _menuService.ObtenerOfertasDeHoy();
        return Ok(ofertas);
    }

    // POST api/ofertas -> crea una oferta nueva
    [HttpPost]
    public IActionResult CrearOferta([FromBody] CrearOfertaRequest request)
    {
        OfertaDelDia ofertaNueva = new OfertaDelDia
        {
            DiaSemana = request.DiaSemana,
            PrecioOferta = request.PrecioOferta
        };

        foreach (int platoId in request.PlatoIds)
        {
            Plato? plato = _db.Platos.Find(platoId);
            if (plato != null)
            {
                ofertaNueva.Platos.Add(plato);
            }
        }

        _db.Ofertas.Add(ofertaNueva);
        _db.SaveChanges();

        return Ok(ofertaNueva);
    }
}