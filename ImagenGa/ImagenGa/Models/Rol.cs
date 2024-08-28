using System.Text.Json.Serialization;

namespace ImagenGa.Models
{
    public class Rol
    {
        public int IdRol { get; set; }
        public string Nombre { get; set; }
        [JsonIgnore]
        public ICollection<Usuario>? Usuario { get; set; }
    }
}
