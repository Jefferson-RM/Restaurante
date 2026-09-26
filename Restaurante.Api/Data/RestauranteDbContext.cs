using Microsoft.EntityFrameworkCore;

public class RestauranteDbContext : DbContext
{
    public RestauranteDbContext(DbContextOptions<RestauranteDbContext> options)
        : base(options) { }

    public DbSet<Plato> Platos => Set<Plato>();
    public DbSet<Combo> Combos => Set<Combo>();
    public DbSet<OfertaDelDia> Ofertas => Set<OfertaDelDia>();
    public DbSet<Pedido> Pedidos => Set<Pedido>();
    public DbSet<ItemPedido> ItemsPedido => Set<ItemPedido>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Plato>().HasData(
            // --- Entradas ---
            new Plato { Id = 1, Nombre = "Tequeños de queso", Precio = 150, Categoria = CategoriaPlato.Entrada },
            new Plato { Id = 2, Nombre = "Ensalada César", Precio = 250, Categoria = CategoriaPlato.Entrada },
            new Plato { Id = 3, Nombre = "Empanaditas de queso", Precio = 120, Categoria = CategoriaPlato.Entrada },
            new Plato { Id = 4, Nombre = "Croquetas de yuca", Precio = 130, Categoria = CategoriaPlato.Entrada },
            new Plato { Id = 5, Nombre = "Chicharrón de pollo", Precio = 200, Categoria = CategoriaPlato.Entrada },

            // --- Platos Fuertes (10) ---
            new Plato { Id = 6, Nombre = "Hamburguesa con Papas", Precio = 500, Categoria = CategoriaPlato.PlatoFuerte },
            new Plato { Id = 7, Nombre = "Pasta con Camarones", Precio = 900, Categoria = CategoriaPlato.PlatoFuerte },
            new Plato { Id = 8, Nombre = "Pizza de Pepperoni", Precio = 1200, Categoria = CategoriaPlato.PlatoFuerte },
            new Plato { Id = 9, Nombre = "Pechuga a la Plancha con Vegetales", Precio = 420, Categoria = CategoriaPlato.PlatoFuerte },
            new Plato { Id = 10, Nombre = "Risotto", Precio = 2500, Categoria = CategoriaPlato.PlatoFuerte },
            new Plato { Id = 11, Nombre = "Mofongo con Camarones", Precio = 850, Categoria = CategoriaPlato.PlatoFuerte },
            new Plato { Id = 12, Nombre = "Chuleta de Cerdo", Precio = 550, Categoria = CategoriaPlato.PlatoFuerte },
            new Plato { Id = 13, Nombre = "Pollo Guisado con Arroz y Habichuelas", Precio = 400, Categoria = CategoriaPlato.PlatoFuerte },
            new Plato { Id = 14, Nombre = "Filete de Pescado a la Plancha", Precio = 750, Categoria = CategoriaPlato.PlatoFuerte },
            new Plato { Id = 15, Nombre = "Rabo Encendido", Precio = 700, Categoria = CategoriaPlato.PlatoFuerte },

            // --- Bebidas ---
            new Plato { Id = 16, Nombre = "Refresco", Precio = 80, Categoria = CategoriaPlato.Bebida },
            new Plato { Id = 17, Nombre = "Jugo Natural", Precio = 90, Categoria = CategoriaPlato.Bebida },
            new Plato { Id = 18, Nombre = "Vino Blanco (copa)", Precio = 1500, Categoria = CategoriaPlato.Bebida },
            new Plato { Id = 19, Nombre = "Vino Tinto (copa)", Precio = 1500, Categoria = CategoriaPlato.Bebida },
            new Plato { Id = 20, Nombre = "Cerveza", Precio = 150, Categoria = CategoriaPlato.Bebida },
            new Plato { Id = 21, Nombre = "Agua Mineral", Precio = 60, Categoria = CategoriaPlato.Bebida },

            // --- Postres (4) ---
            new Plato { Id = 22, Nombre = "Flan", Precio = 400, Categoria = CategoriaPlato.Postre },
            new Plato { Id = 23, Nombre = "Cheesecake", Precio = 400, Categoria = CategoriaPlato.Postre },
            new Plato { Id = 24, Nombre = "Brownie con Helado", Precio = 350, Categoria = CategoriaPlato.Postre },
            new Plato { Id = 25, Nombre = "Tres Leches", Precio = 380, Categoria = CategoriaPlato.Postre }
        );
    }
}