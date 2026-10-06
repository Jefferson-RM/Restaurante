using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class CombosController : ControllerBase
{
    private readonly RestauranteDbContext _db;

    public CombosController(RestauranteDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public IActionResult ObtenerCombos()
    {
        List<Combo> combos = _db.Combos.Include(c => c.Platos).ToList();
        return Ok(combos);
    }

    [Authorize]
    [HttpPost]
    public IActionResult CrearCombo([FromBody] CrearComboRequest request)
    {
        Combo comboNuevo = new Combo
        {
            Nombre = request.Nombre,
            PrecioCombo = request.PrecioCombo
        };

        foreach (int platoId in request.PlatoIds)
        {
            Plato? plato = _db.Platos.Find(platoId);
            if (plato != null)
            {
                comboNuevo.Platos.Add(plato);
            }
        }

        _db.Combos.Add(comboNuevo);
        _db.SaveChanges();

        return Ok(comboNuevo);
    }

    [Authorize]
    [HttpPut("{id}")]
    public IActionResult EditarCombo(int id, [FromBody] CrearComboRequest request)
    {
        Combo? combo = _db.Combos.Include(c => c.Platos).FirstOrDefault(c => c.Id == id);

        if (combo == null)
        {
            return NotFound();
        }

        combo.Nombre = request.Nombre;
        combo.PrecioCombo = request.PrecioCombo;

        combo.Platos.Clear();
        foreach (int platoId in request.PlatoIds)
        {
            Plato? plato = _db.Platos.Find(platoId);
            if (plato != null)
            {
                combo.Platos.Add(plato);
            }
        }

        _db.SaveChanges();

        return Ok(combo);
    }

    [Authorize]
    [HttpDelete("{id}")]
    public IActionResult EliminarCombo(int id)
    {
        Combo? combo = _db.Combos.Find(id);

        if (combo == null)
        {
            return NotFound();
        }

        _db.Combos.Remove(combo);
        _db.SaveChanges();

        return NoContent();
    }
}