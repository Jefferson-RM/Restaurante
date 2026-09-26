using Microsoft.EntityFrameworkCore;

public class MenuService
{
    private readonly RestauranteDbContext _db;

    public MenuService(RestauranteDbContext db)
    {
        _db = db;
    }

    public List<OfertaDelDia> ObtenerOfertasDeHoy()
    {
        DayOfWeek hoy = DateTime.Now.DayOfWeek;

        List<OfertaDelDia> ofertas = _db.Ofertas
            .Include(o => o.Platos)
            .Where(o => o.DiaSemana == hoy)
            .ToList();

        return ofertas;
    }
}