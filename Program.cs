using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using BiteSpot.Data;
using BiteSpot.Services; // 👈 Importante para usar el servicio

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSession();

// ✅ Obtener la cadena de conexión desde variable de entorno (producción) o archivo de configuración (desarrollo)
var connectionString = Environment.GetEnvironmentVariable("RAILWAY_DB_URL")
                      ?? builder.Configuration.GetConnectionString("DefaultConnection");

// ✅ Configurar PostgreSQL como base de datos
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddControllersWithViews();

// ✅ Registro del servicio TendenciaService para aplicar DIP
builder.Services.AddScoped<ITendenciaService, TendenciaService>();

var app = builder.Build();

// Middleware estándar de ASP.NET Core
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();
