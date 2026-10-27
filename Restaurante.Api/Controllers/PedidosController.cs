using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class PedidosController : ControllerBase
{
    private readonly PedidoService _pedidoService;
    private readonly RestauranteDbContext _db;

    public PedidosController(PedidoService pedidoService, RestauranteDbContext db)
    {
        _pedidoService = pedidoService;
        _db = db;
    }

    // Público: el cliente hace su pedido sin loguearse
    [HttpPost]
    public IActionResult CrearPedido([FromBody] CrearPedidoRequest request)
    {
        Pedido pedido = _pedidoService.CrearPedido(request);

        return Ok(new
        {
            pedido.Id,
            pedido.Fecha,
            Subtotal = pedido.CalcularSubtotal(),
            Itbis = pedido.CalcularItbis(),
            PropinaLegal = pedido.CalcularPropinaLegal(),
            Total = pedido.CalcularTotal()
        });
    }

    // Solo admin: lista de pedidos
    [Authorize]
    [HttpGet]
    public IActionResult ObtenerPedidos()
    {
        List<Pedido> pedidos = _db.Pedidos
            .Include(p => p.Items).ThenInclude(i => i.Plato)
            .Include(p => p.Items).ThenInclude(i => i.Combo)
            .OrderByDescending(p => p.Fecha)
            .ToList();

        var resultado = pedidos.Select(p => new
        {
            p.Id,
            p.Fecha,
            p.NombreCliente,
            p.Telefono,
            TipoPedido = p.TipoPedido.ToString(),
            Estado = p.Estado.ToString(),
            Total = p.CalcularTotal(),
            Articulos = p.Items.Select(i => new
            {
                Nombre = i.Plato?.Nombre ?? i.Combo?.Nombre,
                i.Cantidad
            })
        });

        return Ok(resultado);
    }

    // Solo admin: ver un pedido
    [Authorize]
    [HttpGet("{id}")]
    public IActionResult ObtenerPedido(int id)
    {
        Pedido? pedido = _db.Pedidos
            .Include(p => p.Items).ThenInclude(i => i.Plato)
            .Include(p => p.Items).ThenInclude(i => i.Combo)
            .FirstOrDefault(p => p.Id == id);

        if (pedido == null)
        {
            return NotFound();
        }

        return Ok(pedido);
    }

    // Solo admin: cambiar el estado del pedido
    [Authorize]
    [HttpPut("{id}/estado")]
    public IActionResult CambiarEstado(int id, [FromBody] CambiarEstadoRequest request)
    {
        Pedido? pedido = _db.Pedidos.Find(id);

        if (pedido == null)
        {
            return NotFound();
        }

        pedido.Estado = request.Estado;
        _db.SaveChanges();

        return Ok(new { pedido.Id, Estado = pedido.Estado.ToString() });
    }
}