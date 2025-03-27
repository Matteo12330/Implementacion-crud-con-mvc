using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Implementacion_crud_con_mvc.Models
{
    public class Libro
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Titulo { get; set; } = string.Empty;

        [Required]
        public string Autor { get; set; } = string.Empty;

        [Required]
        public DateTime FechaPublicacion { get; set; }

        [ForeignKey("Tendencia")]
        public int TendenciaId { get; set; } // Llave foránea hacia Tendencia

        public Tendencia? Tendencia { get; set; } // Relación con la tabla Tendencias
    }
}
