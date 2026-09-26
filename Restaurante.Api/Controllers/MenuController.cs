using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class MenuController : ControllerBase
{
    private readonly RestauranteDbContext _db;
    private readonly MenuService _menuService;

    public MenuController(RestauranteDbContext db, MenuService menuService)
    {
        _db = db;
        _menuService = menuService;
    }

    // GET api/menu -> devuelve todos los platos
    [HttpGet]
    public IActionResult ObtenerMenu()
    {
        List<Plato> platos = _db.Platos.ToList();
        return Ok(platos);
    }

    // GET api/menu/oferta-hoy -> devuelve las ofertas activas hoy
    [HttpGet("oferta-hoy")]
    public IActionResult ObtenerOfertaHoy()
    {
        List<OfertaDelDia> ofertas = _menuService.ObtenerOfertasDeHoy();
        return Ok(ofertas);
    }
}