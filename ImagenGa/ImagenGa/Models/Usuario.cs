using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ImagenGa.Models;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string? Nombre { get; set; }

    public string? Correo { get; set; }
    public int IdRol { get; set; }

    
    public string? Clave { get; set; }
    [JsonIgnore]
    public ICollection<Registro>? Registro { get; set; }
    [JsonIgnore]
    public Rol? Rol { get; set; }
}
