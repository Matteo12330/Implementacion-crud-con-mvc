using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace BiteSpot.Models
{
    public class Opinion
    {
        [Key]
        public int Id { get; set; } // ID único para cada opinión

        [Required]
        [Range(1, 5)]
        public int Puntuacion { get; set; } // Calificación del 1 al 5

        [StringLength(500)]
        public string? Comentario { get; set; }

        public DateTime Fecha { get; set; } = DateTime.Now; // Fecha en que se deja la opinión

        [Required]
        public int ProductoId { get; set; } // Relación con el producto opinado

        [ForeignKey("ProductoId")]
        [ValidateNever]
        public Producto Producto { get; set; } = null!; // Producto al que pertenece esta opinión

        [ValidateNever]
        public int UsuarioId { get; set; } // Usuario que dejó la opinión

        [ForeignKey("UsuarioId")]
        [ValidateNever]
        public Usuario Usuario { get; set; } = null!; // Tenemos la relación con el usuario que opinó
    }
}