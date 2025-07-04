using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BiteSpot.Models
{
    public class Producto
    {
        public int Id { get; set; } // ID único del producto

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(100)]
        public string? Nombre { get; set; }

        [Required(ErrorMessage = "El precio es obligatorio")]
        [Range(0.01, 10000, ErrorMessage = "El precio debe estar entre 0.01 y 10,000")]
        public decimal Precio { get; set; }

        [Display(Name = "Disponible desde")]
        [DataType(DataType.Date)]
        public DateTime FechaLanzamiento { get; set; } = DateTime.UtcNow;

        [StringLength(500)]
        public string? Descripcion { get; set; }

        [StringLength(300)]
        public string ImagenUrl { get; set; } = string.Empty; // ✅ Nueva propiedad agregada

        public int? TendenciaId { get; set; }

        [ForeignKey("TendenciaId")]
        public Tendencia? Tendencia { get; set; }

        [Required(ErrorMessage = "Debes seleccionar una categoría")]
        public int CategoriaId { get; set; }

        [ForeignKey("CategoriaId")]
        public Categoria? Categoria { get; set; }

        public ICollection<Opinion> Opiniones { get; set; } = new List<Opinion>();

        [NotMapped]
        public double PromedioCalificacion { get; set; } // Calculado dinámicamente
    }
}
