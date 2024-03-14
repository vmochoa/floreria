using System;
using System.Collections.Generic;

namespace FloreriaMvc.Models;

public partial class Insumo
{
    public int InsumoId { get; set; }

    public string Nombre { get; set; } = null!;

    public decimal Costo { get; set; }

    public int UnidadesPorPaquete { get; set; }

    public decimal CostoUnitario { get; set; }

    public virtual ICollection<InsumoProducto> InsumoProductos { get; set; } = new List<InsumoProducto>();
}
