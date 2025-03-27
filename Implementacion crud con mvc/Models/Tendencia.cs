using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Implementacion_crud_con_mvc.Models
{
    public class Tendencia
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Nombre { get; set; } = string.Empty;

        public string Descripcion { get; set; } = string.Empty; // Agregar esta propiedad
        public DateTime FechaCreacion { get; set; } = DateTime.Now; // Agregar esta propiedad

        public ICollection<Libro> Libros { get; set; } = new List<Libro>(); // Relación uno a muchos con Libro
    }
}
