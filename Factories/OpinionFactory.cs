using BiteSpot.Models;

namespace BiteSpot.Factories
{
    public static class OpinionFactory
    {
        public static Opinion CrearOpinion(int usuarioId, int productoId, string comentario, int puntuacion)
        {
            return new Opinion
            {
                UsuarioId = usuarioId,
                ProductoId = productoId,
                Comentario = comentario,
                Puntuacion = puntuacion,
                Fecha = DateTime.UtcNow
            };
        }
    }
}
