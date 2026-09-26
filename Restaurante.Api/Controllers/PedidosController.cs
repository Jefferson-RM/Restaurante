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

    // POST api/pedidos -> crea un pedido nuevo
    [HttpPost]
    public IActionResult CrearPedido([FromBody] CrearPedidoRequest request)
    {
        Pedido pedido = _pedidoService.CrearPedido(request.Items);

        var respuesta = new
        {
            pedido.Id,
            pedido.Fecha,
            Subtotal = pedido.CalcularSubtotal(),
            Itbis = pedido.CalcularItbis(),
            PropinaLegal = pedido.CalcularPropinaLegal(),
            Total = pedido.CalcularTotal()
        };

        return Ok(respuesta);
    }

    // GET api/pedidos/{id} -> consulta un pedido ya creado
    [HttpGet("{id}")]
    public IActionResult ObtenerPedido(int id)
    {
        Pedido? pedido = _db.Pedidos
            .Include(p => p.Items)
            .FirstOrDefault(p => p.Id == id);

        if (pedido == null)
        {
            return NotFound();
        }

        return Ok(pedido);
    }
}