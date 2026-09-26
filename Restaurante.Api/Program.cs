using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// --- Registro de servicios (le decimos a ASP.NET qué piezas existen) ---
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<RestauranteDbContext>(options =>
    options.UseSqlite("Data Source=restaurante.db"));

builder.Services.AddScoped<MenuService>();
builder.Services.AddScoped<PedidoService>();
builder.Services.AddScoped<DashboardService>();

var app = builder.Build();

// --- Crea la base de datos automáticamente si no existe ---
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<RestauranteDbContext>();
    db.Database.EnsureCreated();
}

// --- Middleware (lo que pasa con cada petición que llega) ---
app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();