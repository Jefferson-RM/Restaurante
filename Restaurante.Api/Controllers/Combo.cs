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

    // GET api/combos -> lista todos los combos existentes
    [HttpGet]
    public IActionResult ObtenerCombos()
    {
        List<Combo> combos = _db.Combos.Include(c => c.Platos).ToList();
        return Ok(combos);
    }

    // POST api/combos -> crea un combo nuevo
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

    // PUT api/combos/{id} -> edita un combo existente
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

    // DELETE api/combos/{id} -> elimina un combo
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