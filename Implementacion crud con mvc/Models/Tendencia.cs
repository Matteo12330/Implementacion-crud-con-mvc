using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Implementacion_crud_con_mvc.Models
{
    public class Tendencia
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(50, ErrorMessage = "Máximo 50 caracteres")]
        public string Nombre { get; set; } = string.Empty;

        [StringLength(300)]
        public string Descripcion { get; set; } = string.Empty;

        [DataType(DataType.Date)]
        public DateTime FechaCreacion { get; set; } = DateTime.Now;

        public ICollection<Producto> Productos { get; set; } = new List<Producto>();

        [Required(ErrorMessage = "Debe seleccionar una categoría")]
        public int CategoriaId { get; set; }

        [ForeignKey("CategoriaId")]
        [ValidateNever]
        public Categoria Categoria { get; set; } = null!;
    }
}
