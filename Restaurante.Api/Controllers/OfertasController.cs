using Microsoft.AspNetCore.Authorization;
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

    [HttpGet]
    public IActionResult ObtenerOfertas()
    {
        List<OfertaDelDia> ofertas = _db.Ofertas.Include(o => o.Platos).ToList();
        return Ok(ofertas);
    }

    [HttpGet("hoy")]
    public IActionResult ObtenerOfertasDeHoy()
    {
        List<OfertaDelDia> ofertas = _menuService.ObtenerOfertasDeHoy();
        return Ok(ofertas);
    }

    [Authorize]
    [HttpPost]
    public IActionResult CrearOferta([FromBody] CrearOfertaRequest request)
    {
        OfertaDelDia ofertaNueva = new OfertaDelDia
        {
            FechaInicio = request.FechaInicio,
            FechaFin = request.FechaFin,
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

    [Authorize]
    [HttpPut("{id}")]
    public IActionResult EditarOferta(int id, [FromBody] CrearOfertaRequest request)
    {
        OfertaDelDia? oferta = _db.Ofertas.Include(o => o.Platos).FirstOrDefault(o => o.Id == id);

        if (oferta == null)
        {
            return NotFound();
        }

        oferta.FechaInicio = request.FechaInicio;
        oferta.FechaFin = request.FechaFin;
        oferta.PrecioOferta = request.PrecioOferta;

        oferta.Platos.Clear();
        foreach (int platoId in request.PlatoIds)
        {
            Plato? plato = _db.Platos.Find(platoId);
            if (plato != null)
            {
                oferta.Platos.Add(plato);
            }
        }

        _db.SaveChanges();

        return Ok(oferta);
    }

    [Authorize]
    [HttpDelete("{id}")]
    public IActionResult EliminarOferta(int id)
    {
        OfertaDelDia? oferta = _db.Ofertas.Find(id);

        if (oferta == null)
        {
            return NotFound();
        }

        _db.Ofertas.Remove(oferta);
        _db.SaveChanges();

        return NoContent();
    }
}