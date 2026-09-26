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
}