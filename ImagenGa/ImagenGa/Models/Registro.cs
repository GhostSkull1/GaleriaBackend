using Microsoft.EntityFrameworkCore;

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ImagenGa.Models
{
    public class Registro
    {
        
        public int IdRegistro { get; set; }
        public int IdImagen { get; set; }
        public int IdUsuario { get; set; }
        public string IdAccion { get; set; }
        public string? Descripcion { get; set; }

        public DateTime Fecha { get; set; }

        [JsonIgnore]
        public Imagen? Imagen { get; set; }
        [JsonIgnore]
        public Usuario? Usuario { get; set; }
    }
}
