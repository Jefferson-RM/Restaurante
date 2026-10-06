using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
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

    // PUT api/platos/{id} -> edita un plato existente
    [Authorize]
    [HttpPut("{id}")]
    public IActionResult EditarPlato(int id, [FromBody] CrearPlatoRequest request)
    {
        Plato? plato = _db.Platos.Find(id);

        if (plato == null)
        {
            return NotFound();
        }

        plato.Nombre = request.Nombre;
        plato.Precio = request.Precio;
        plato.Categoria = request.Categoria;

        _db.SaveChanges();

        return Ok(plato);
    }

    // DELETE api/platos/{id} -> elimina un plato
    [Authorize]
    [HttpDelete("{id}")]
    public IActionResult EliminarPlato(int id)
    {
        Plato? plato = _db.Platos.Find(id);

        if (plato == null)
        {
            return NotFound();
        }

        _db.Platos.Remove(plato);
        _db.SaveChanges();

        return NoContent();
    }
}