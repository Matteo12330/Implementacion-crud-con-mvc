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
        public string? Nombre { get; set; } // Nombre del producto

        [Required(ErrorMessage = "El precio es obligatorio")]
        [Range(0.01, 10000, ErrorMessage = "El precio debe estar entre 0.01 y 10,000")]
        public decimal Precio { get; set; } // Precio con validación de rango

        [Display(Name = "Disponible desde")]
        [DataType(DataType.Date)]
        public DateTime FechaLanzamiento { get; set; } // Fecha en que se lanza el producto

        [StringLength(500)]
        public string? Descripcion { get; set; } // Descripción opcional del producto

        public int? TendenciaId { get; set; } // Tendencia generada automáticamente si el producto es popular

        [ForeignKey("TendenciaId")]
        public Tendencia? Tendencia { get; set; } // Relación con la tabla Tendencias (nullable)

        [Required(ErrorMessage = "Debes seleccionar una categoría")]
        public int CategoriaId { get; set; } // Categoría seleccionada al crear el producto

        [ForeignKey("CategoriaId")]
        public Categoria? Categoria { get; set; } // Relación con la tabla Categorías

        public ICollection<Opinion> Opiniones { get; set; } = new List<Opinion>(); // Opiniones dejadas por los usuarios

        [NotMapped]
        public double PromedioCalificacion { get; set; } // Se calcula al consultar, no se guarda en la base
    }
}