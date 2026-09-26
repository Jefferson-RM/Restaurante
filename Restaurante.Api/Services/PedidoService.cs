using Microsoft.EntityFrameworkCore;

public class PedidoService
{
    private readonly RestauranteDbContext _db;
    private readonly MenuService _menuService;

    public PedidoService(RestauranteDbContext db, MenuService menuService)
    {
        _db = db;
        _menuService = menuService;
    }

    public ItemPedido CrearItemParaPlato(int platoId, int cantidad)
    {
        Plato plato = _db.Platos.Find(platoId)!;

        List<OfertaDelDia> ofertasHoy = _menuService.ObtenerOfertasDeHoy();

        // Buscamos si ALGUNA oferta de hoy incluye este plato específico
        OfertaDelDia? ofertaAplicable = ofertasHoy
            .FirstOrDefault(o => o.Platos.Any(p => p.Id == platoId));

        ItemPedido item = new ItemPedido
        {
            Plato = plato,
            Cantidad = cantidad
        };

        if (ofertaAplicable != null)
        {
            item.TipoDeVenta = TipoDeVenta.Oferta;
            item.PrecioUnitarioAplicado = ofertaAplicable.PrecioOferta;
        }
        else
        {
            item.TipoDeVenta = TipoDeVenta.Normal;
            item.PrecioUnitarioAplicado = plato.Precio;
        }

        return item;
    }

    public ItemPedido CrearItemParaCombo(int comboId, int cantidad)
    {
        Combo combo = _db.Combos.Include(c => c.Platos).First(c => c.Id == comboId);

        ItemPedido item = new ItemPedido
        {
            Combo = combo,
            Cantidad = cantidad,
            TipoDeVenta = TipoDeVenta.Combo,
            PrecioUnitarioAplicado = combo.PrecioCombo
        };

        return item;
    }

    public Pedido CrearPedido(List<ItemSolicitado> itemsSolicitados)
    {
        Pedido pedido = new Pedido();

        foreach (ItemSolicitado solicitud in itemsSolicitados)
        {
            if (solicitud.PlatoId.HasValue)
            {
                ItemPedido item = CrearItemParaPlato(solicitud.PlatoId.Value, solicitud.Cantidad);
                pedido.Items.Add(item);
            }
            else if (solicitud.ComboId.HasValue)
            {
                ItemPedido item = CrearItemParaCombo(solicitud.ComboId.Value, solicitud.Cantidad);
                pedido.Items.Add(item);
            }
        }

        _db.Pedidos.Add(pedido);
        _db.SaveChanges();

        return pedido;
    }
}