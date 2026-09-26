using Microsoft.AspNetCore.Mvc;

public class CrearPedidoRequest
{
    public List<ItemSolicitado> Items { get; set; } = new List<ItemSolicitado>();
}