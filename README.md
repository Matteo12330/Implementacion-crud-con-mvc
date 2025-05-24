# BiteSpot – Aplicación Web Gastronómica (ASP.NET MVC - Proyecto UDLA)

BiteSpot es una aplicación web desarrollada con **ASP.NET Core MVC**, que simula una plataforma gastronómica interactiva enfocada en la visualización, valoración y promoción de productos mediante la participación activa de los usuarios. A diferencia de una tienda en línea, BiteSpot **no gestiona ventas ni pagos**, sino que su objetivo es resaltar automáticamente los productos más populares a través de un flujo personalizado basado en **opiniones** y **calificaciones** de los usuarios.

El sistema permite a los usuarios registrados explorar productos destacados, dejar reseñas y contribuir a la generación automática de tendencias. A su vez, los administradores pueden gestionar productos, categorías y tendencias desde un panel de control.

---

## 🔄 Fase 1: CRUD básico (productos y tendencias) - 31 de Marzo 2025

- Creación, lectura, actualización y eliminación (CRUD) de productos y tendencias.
- Implementado con Entity Framework y SQL Server LocalDB.
- Interfaz inicial estructurada en Razor y Bootstrap.
- Link Youtube: https://youtu.be/OI409buKkPw?si=9Z5LGMbDoOUtvIJs

---

## 🔐 Fase 2: Login seguro y control de sesiones - 7 de abril 2025

- Registro e inicio de sesión de usuarios.
- Validación de contraseñas seguras (mínimo 8 caracteres, mayúscula, número).
- Encriptación con MD5 para almacenar las contraseñas.
- Almacenamiento de sesión y navegación protegida con `[LoginAuthorize]`.
- Rediseño visual con enfoque realista orientado a marca (BiteSpot).
- Link Youtube: https://youtu.be/K9hS8q5tFEA?si=33YZUF52_8Uo8vdk

---

## 🚀 Fase 3: Validaciones Back-End, Dropdowns, SQLite y Deploy - 7 de mayo 2025

Esta tercera fase introduce mejoras clave para preparar el proyecto a nivel de producción:

### ✅ Mejoras implementadas

- **Validaciones en Back-End**: asegurando integridad de datos sensibles (campos obligatorios, claves foráneas, email único, etc.).
- **Dropdowns dependientes**: se añadió relación Categoría → Tendencia en el formulario de creación de productos.
- **Compatibilidad dual** de bases de datos:
  - SQL Server en entorno local (desarrollo).
  - SQLite en entorno productivo (Render).
- **Dockerización del proyecto** para despliegue en Render.
- Archivos añadidos:
  - `Dockerfile`
  - `.dockerignore`
  - `README.md` documentado
- Lógica automática para detectar entorno (`Program.cs`) y ajustar la conexión según corresponda.
- Link Youtube: https://youtu.be/QMFov7ySAqg?si=F8LOVsRb--xRxOf8

---

## 🧠 Lógica del Core Propuesto

La lógica central de BiteSpot se basa en generar **tendencias automáticas** en función de las opiniones y calificaciones de los usuarios:

- Cada opinión tiene una puntuación (de 1 a 5 estrellas).
- El sistema evalúa el promedio de cada producto y la cantidad de opiniones.
- Se genera un ranking dinámico de productos populares.
- Los productos mejor valorados se destacan automáticamente en el Home.

> Este flujo se alinea con el objetivo académico: demostrar un "core personalizado" que se genera automáticamente a partir de la interacción de al menos dos tablas relacionadas (Producto y Opinión).

---

## 🛠 Tecnologías Utilizadas

- ASP.NET Core MVC 8
- Entity Framework Core
- SQL Server LocalDB (desarrollo)
- SQLite (producción en Render)
- Bootstrap 5
- Sesiones con `HttpContext.Session`
- Encriptación de contraseñas con MD5
- Docker + Render

---

## 📁 Estructura del Proyecto

- **/Models**: Usuario, Producto, Tendencia, Categoria, Opinión
- **/Controllers**: ProductosController, TendenciaController, CategoriaController, AccountController
- **/Views**: CRUD + login/register
- **/Helpers**: `SeguridadHelper.cs`, `LoginAuthorizeAttribute.cs`
- **/Data**: `ApplicationDbContext.cs`
- **/Migrations**: SQL Server y SQLite
- **Raíz del proyecto**:
  - `Dockerfile`
  - `.dockerignore`
  - `README.md`

---

## ⚙️ Configuración de conexión por entorno

En `Program.cs`:

```csharp
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetSection("ConnectionStrings_Local")["DefaultConnection"]));
}
else
{
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
}

Y en appsettings.json:

"ConnectionStrings": {
  "DefaultConnection": "Data Source=app.db"
},
"ConnectionStrings_Local": {
  "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=ImplementacionCrudMVC;Trusted_Connection=True;"
}
