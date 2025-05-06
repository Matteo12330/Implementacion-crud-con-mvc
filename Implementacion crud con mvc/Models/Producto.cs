using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Implementacion_crud_con_mvc.Models
{
    public class Producto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(100)]
        public string? Nombre { get; set; }

        [Required(ErrorMessage = "El precio es obligatorio")]
        [Range(0.01, 10000, ErrorMessage = "El precio debe estar entre 0.01 y 10,000")]
        public decimal Precio { get; set; }

        [Display(Name = "Disponible desde")]
        [DataType(DataType.Date)]
        public DateTime FechaLanzamiento { get; set; }

        [StringLength(500)]
        public string? Descripcion { get; set; }

        [Required(ErrorMessage = "Debes seleccionar una tendencia")]
        [Display(Name = "Tendencia")]
        public int TendenciaId { get; set; }

        [ForeignKey("TendenciaId")]
        public Tendencia? Tendencia { get; set; }

        [Required(ErrorMessage = "Debes seleccionar una categoría")]
        [Display(Name = "Categoría")]
        public int CategoriaId { get; set; }

        [ForeignKey("CategoriaId")]
        public Categoria? Categoria { get; set; }
    }
}
