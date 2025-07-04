# BiteSpot – Aplicación Web Gastronómica (ASP.NET MVC - Proyecto UDLA)

**BiteSpot** es una aplicación web desarrollada con **ASP.NET Core MVC**, que simula una plataforma gastronómica interactiva enfocada en la visualización, valoración y promoción de productos mediante la participación activa de los usuarios.

🎯 A diferencia de una tienda online, **BiteSpot no gestiona ventas ni pagos**. Su objetivo es **resaltar automáticamente los productos más populares** a través de un flujo personalizado basado en opiniones y calificaciones de los usuarios.

Los usuarios registrados pueden explorar productos, dejar reseñas y contribuir a la generación automática de tendencias. Los administradores gestionan productos, categorías y tendencias desde un panel privado.

---

## 🚧 Fases del Proyecto

### 🔄 Fase 1: CRUD básico (31 de marzo 2025)
- CRUD de productos y tendencias con Entity Framework y SQL Server.
- Interfaz inicial estructurada con Razor y Bootstrap.  
✅ [Ver en YouTube](https://youtu.be/OI409buKkPw?si=9Z5LGMbDoOUtvIJs)

### 🔐 Fase 2: Login seguro y control de sesiones (7 de abril 2025)
- Registro e inicio de sesión de usuarios.
- Validación de contraseñas seguras.
- Encriptación MD5.
- Protección de rutas con `[LoginAuthorize]`.
- Rediseño visual orientado a marca.  
✅ [Ver en YouTube](https://youtu.be/K9hS8q5tFEA?si=33YZUF52_8Uo8vdk)

### 🚀 Fase 3: Validaciones, dropdowns, SQLite y Docker (7 de mayo 2025)
- Validaciones en back-end: campos obligatorios, email único, claves foráneas.
- Dropdowns anidados (Categoría → Tendencia).
- Soporte para dos entornos de base de datos:
  - SQL Server (Desarrollo)
  - SQLite (Producción inicial)
- Dockerización completa del proyecto.
- README.md técnico incluido.  
✅ [Ver en YouTube](https://youtu.be/QMFov7ySAqg?si=F8LOVsRb--xRxOf8)

### 🌟 Fase 4: Core personalizado, mejoras y despliegue final (26 de mayo 2025)

#### ✅ Avances finales
- Sistema de opiniones con calificaciones (1 a 5 estrellas).
- Cálculo automático del promedio por producto.
- Si el promedio es ≥ 4.0 y no tiene tendencia asignada, el sistema le asigna automáticamente la tendencia **"Favoritos de los usuarios"**.
- Se guarda la fecha de generación de la tendencia.
- Control para evitar que se genere la misma tendencia más de una vez.
- Vista de detalles muestra la fecha exacta en que se generó la tendencia.
- Separación de vistas por rol (`Session["Rol"]`).
- Restricción para que solo usuarios normales puedan dejar opiniones.
- El administrador puede eliminar opiniones para moderación.
- Migración a PostgreSQL como base de datos en producción.
- Despliegue exitoso en Render con Railway.

#### ✅ Sistema en producción  
🔗 https://bitespot.onrender.com

---

## 🧠 Lógica del Core Personalizado

1. Los usuarios califican productos con 1 a 5 estrellas.
2. El sistema recalcula el promedio tras cada nueva opinión.
3. Si el promedio es ≥ 4.0 y el producto no tiene tendencia:
   - Se asigna automáticamente **“Favoritos de los usuarios”**.
   - Se guarda la fecha de asignación.
4. En la vista de detalles se muestra la tendencia y la fecha.
5. Este flujo cumple con el objetivo académico de generar una lógica personalizada basada en al menos dos entidades relacionadas (`Producto` y `Opinión`).

---

## 🧠 Mejores Prácticas Aplicadas (SOLID + Patrones)

Como parte de la fase final, se aplicaron **2 principios SOLID y 2 patrones de diseño** para mejorar la estructura y calidad del código:

### ✅ Principios SOLID

- **SRP (Responsabilidad Única)**  
  Se creó la clase `TendenciaService.cs` para encapsular toda la lógica de generación de tendencias, separándola del controlador.

- **DIP (Inversión de Dependencias)**  
  El controlador `OpinionController.cs` depende de la interfaz `ITendenciaService`, que se inyecta desde `Program.cs`. Esto permite mayor flexibilidad, desacoplamiento y testeo.

### 🧱 Patrones de Diseño

- **Repository Pattern**  
  `TendenciaService` actúa como repositorio, encapsulando el acceso a la base de datos y reglas de negocio relacionadas a tendencias y productos.

- **Factory Pattern**  
  `OpinionFactory.cs` permite crear instancias de opiniones con todos sus campos ya inicializados (`UsuarioId`, `Puntuacion`, `Fecha`), centralizando esa lógica y evitando duplicación.

---

## 🛠 Tecnologías Utilizadas

- ASP.NET Core MVC 8  
- Entity Framework Core  
- SQL Server LocalDB (Desarrollo)  
- PostgreSQL (Producción vía Railway)  
- Docker + Render  
- Bootstrap 5  
- Razor Pages  
- Autenticación con sesiones (`HttpContext.Session`)  
- Validaciones en back-end  
- Encriptación de contraseñas (MD5)

---

## 📁 Estructura del Proyecto

/Models

Usuario.cs

Producto.cs

Tendencia.cs

Categoria.cs

Opinion.cs

/Controllers

ProductosController.cs

TendenciaController.cs

CategoriaController.cs

OpinionController.cs

AccountController.cs

/Views

Productos/

Opiniones/

Tendencias/

Account/

Shared/

/Helpers

SeguridadHelper.cs

LoginAuthorizeAttribute.cs

TendenciaHelper.cs

/Services

ITendenciaService.cs

TendenciaService.cs

/Factories

OpinionFactory.cs

/Data

ApplicationDbContext.cs

/Migrations

SQL Server y PostgreSQL

Raíz del proyecto:

Dockerfile

.dockerignore

README.md

Program.cs

appsettings.json

---

## ⚙️ Configuración de conexión por entorno

**En `Program.cs`:**

```csharp
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetSection("ConnectionStrings_Local")["DefaultConnection"]));
}
else
{
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
}
En appsettings.json:

json
Copiar
Editar
"ConnectionStrings": {
  "DefaultConnection": "Host=your_postgres_host;Port=5432;Database=bitespot;Username=postgres;Password=your_password"
},
"ConnectionStrings_Local": {
  "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=BiteSpot;Trusted_Connection=True;"
}



✅ Estado actual
✔️ Core funcional implementado

✔️ Opiniones conectadas y funcionando

✔️ Generación automática de tendencias

✔️ Separación de vistas por rol

✔️ Validaciones completas y control de acceso

✔️ Docker y documentación técnica incluidos

✔️ Proyecto desplegado en la nube con PostgreSQL
