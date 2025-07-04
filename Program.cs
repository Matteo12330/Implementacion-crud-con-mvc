using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using BiteSpot.Data;
using BiteSpot.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSession();

// ✅ Cadena de conexión: Railway o local
var connectionString = Environment.GetEnvironmentVariable("RAILWAY_DB_URL")
                      ?? builder.Configuration.GetConnectionString("DefaultConnection");

// ✅ PostgreSQL
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

// ✅ MVC y API
builder.Services.AddControllersWithViews();
builder.Services.AddControllers(); // <-- Si quieres usar solo AddControllersWithViews también sirve, pero este es explícito

// ✅ Servicio
builder.Services.AddScoped<ITendenciaService, TendenciaService>();

var app = builder.Build();

// ✅ Middleware
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

// ✅ Mapeo de rutas MVC
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

// ✅ Mapeo de rutas API
app.MapControllers(); // ⬅️ Esto faltaba

app.Run();