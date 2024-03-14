using System;
using System.Collections.Generic;

namespace FloreriaMvc.Models;

public partial class InsumoProducto
{
    public int InsumoProductoId { get; set; }

    public decimal CantidadNecesaria { get; set; }

    public int InsumoId { get; set; }

    public int ProductoFinalId { get; set; }

    public virtual Insumo Insumo { get; set; } = null!;

    public virtual ProductosFinale ProductoFinal { get; set; } = null!;
}
