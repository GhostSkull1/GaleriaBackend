using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ImagenGa.Models;

public partial class Imagen
{
    public int IdImagen { get; set; }

    public int NumeroDescargas { get; set; }

    public string? Nombre { get; set; }

    public string? Descripcion { get; set; }
    public DateTime FechaC { get; set; }

    public DateTime? FechaE { get; set; }

    public string? ImagenUrl { get; set; }
    [JsonIgnore]
    public ICollection<Registro>? Registro { get; set; }

}
