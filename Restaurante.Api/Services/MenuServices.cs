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
        DateTime hoy = DateTime.Now.Date;

        List<OfertaDelDia> ofertas = _db.Ofertas
            .Include(o => o.Platos)
            .Where(o => hoy >= o.FechaInicio.Date && hoy <= o.FechaFin.Date)
            .ToList();

        return ofertas;
    }
}