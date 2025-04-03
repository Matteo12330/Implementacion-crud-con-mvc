# Implementación de CRUD con ASP.NET MVC

Este proyecto es una aplicación web que implementa las operaciones CRUD (Crear, Leer, Actualizar y Eliminar) utilizando ASP.NET MVC, Entity Framework y SQL Server. El sistema está diseñado para la gestión de libros en una librería, permitiendo registrar, modificar y eliminar información de cada libro disponible. Además, integra la funcionalidad de tendencias, lo que permitirá en el futuro personalizar el flujo de recomendaciones basado en las preferencias y popularidad de los libros. Esta estructura sentará las bases para una aplicación más avanzada con un sistema de flujo personalizado al finalizar el semestre.

## Tecnologías Utilizadas
- ASP.NET MVC (C#)
- SQL Server con Entity Framework

## Funcionalidades
- Gestión de **Tendencias** y **Libros**
- CRUD completo: Crear, Leer, Actualizar y Eliminar
- Validaciones en formularios
- Diseño basado en MVC

## Estructura del Proyecto
- **Models/** → Contiene las clases 'Libro' y 'Tendencia' que representan las tablas en la base de datos.
- **Controllers/** → 'LibroController' y 'TendenciaController' manejan la lógica de negocio y la comunicación con la base de datos.
- **Views/** → Contiene las vistas para mostrar los datos y los formularios.
- **Data/** – Contiene ApplicationDbContext.cs, que define la conexión con la base de datos y configura las tablas.

## Instalación y Configuración
1. Clona este repositorio
2. Abre el proyecto en Visual Studio.
3. Asegúrate de tener SQL Server en ejecución.
4. Ejecuta las migraciones en la consola de NuGet:
5. Corre la aplicación desde Visual Studio.

## Commits Clave
1. Inicialización del Proyecto - Creación de estructura MVC.  
2. Implementación del CRUD - Controladores, modelos y vistas funcionales.  
3. Optimización del Código - Eliminación de lineas, comentarios inecesario, debug y mejoras de legibilidad.  

Puedes ver una demostración del proyecto en el siguiente video: https://youtu.be/OI409buKkPw?si=9Z5LGMbDoOUtvIJs
