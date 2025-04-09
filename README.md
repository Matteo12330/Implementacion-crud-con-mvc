# Implementación de CRUD con ASP.NET MVC

Este proyecto es una aplicación web que implementa operaciones CRUD (Crear, Leer, Actualizar y Eliminar) utilizando **ASP.NET MVC**, **Entity Framework** y **SQL Server**. Fue desarrollado como parte de una práctica académica con el objetivo de aplicar los conceptos del patrón Modelo-Vista-Controlador (MVC), la interacción con bases de datos relacionales y la validación de formularios.

---

## 🆕 Actualización - Login Seguro y Transición a BiteSpot

En esta segunda fase del proyecto se incorporaron nuevas funcionalidades esenciales:

- **Sistema de autenticación de usuarios (Login/Register)** con encriptación MD5 y gestión de sesión.
- **Protección de rutas** mediante un atributo personalizado `[LoginAuthorize]`.
- **Rediseño visual de la interfaz** inspirado en mi marca **BiteSpot**, simulando una aplicación real para un negocio gastronómico (estilo restaurante/kiosko).

> Aunque el dominio original era la gestión de libros y tendencias, se migró visualmente a productos alimenticios (hamburguesas, bebidas, etc.) como parte de un rediseño completo orientado a simular un proyecto real para mi negocio.

---

## ✅ Funcionalidades

- CRUD completo para Tendencias y Productos
- Registro e inicio de sesión de usuarios
- Validación de contraseñas seguras (mínimo 8 caracteres, mayúsculas y números)
- Encriptación de contraseñas con MD5
- Almacenamiento de sesión y navegación protegida
- Visual renovado y responsivo con Bootstrap 5

---

## Tecnologías Utilizadas

- ASP.NET MVC (C#)
- Entity Framework Core
- SQL Server (LocalDB)
- Bootstrap 5
- Validaciones con Data Annotations
- Sesiones con `HttpContext.Session`
- Encriptación de contraseñas (MD5)

---

## Estructura del Proyecto

- **Models/** → `Usuario.cs`, `Producto.cs`, `Tendencia.cs`
- **Controllers/** → `AccountController`, `ProductosController`, `TendenciaController`
- **Views/** → CRUD para Producto y Tendencia, login personalizado
- **Helpers/** → `SeguridadHelper.cs`, `LoginAuthorizeAttribute.cs`
- **Data/** → `ApplicationDbContext.cs` con conexión a base de datos




