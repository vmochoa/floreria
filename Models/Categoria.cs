using System;
using System.Collections.Generic;

namespace FloreriaMvc.Models;

public partial class Categoria
{
    public int CategoriaId { get; set; }

    public string NombreCategoria { get; set; } = null!;

    public virtual ICollection<ProductosFinale> ProductosFinales { get; set; } = new List<ProductosFinale>();
}
