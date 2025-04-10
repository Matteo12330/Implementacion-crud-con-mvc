﻿using System;
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

        public string Descripcion { get; set; } = string.Empty;
        public DateTime FechaCreacion { get; set; } = DateTime.Now;

        // 🔥 Relación inversa: una tendencia puede tener muchos productos
        public ICollection<Producto> Productos { get; set; } = new List<Producto>();
    }
}
