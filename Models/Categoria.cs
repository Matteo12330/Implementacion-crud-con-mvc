using System.ComponentModel.DataAnnotations;

namespace BiteSpot.Models
{
    public class Categoria
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; } = string.Empty; // Inicializado

        [StringLength(255)]
        public string? Descripcion { get; set; }

        // Inicializar colección para evitar advertencia
        public ICollection<Tendencia> Tendencias { get; set; } = new List<Tendencia>();
    }
}
