using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class PlatosController : ControllerBase
{
    private readonly RestauranteDbContext _db;

    public PlatosController(RestauranteDbContext db)
    {
        _db = db;
    }

    // POST api/platos -> crea un plato nuevo
    [HttpPost]
    public IActionResult CrearPlato([FromBody] CrearPlatoRequest request)
    {
        Plato platoNuevo = new Plato
        {
            Nombre = request.Nombre,
            Precio = request.Precio,
            Categoria = request.Categoria
        };

        _db.Platos.Add(platoNuevo);
        _db.SaveChanges();

        return Ok(platoNuevo);
    }
}