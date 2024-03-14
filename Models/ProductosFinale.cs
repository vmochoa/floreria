using System;
using System.Collections.Generic;

namespace FloreriaMvc.Models;

public partial class ProductosFinale
{
    public int ProductoFinalId { get; set; }

    public string Descripcion { get; set; } = null!;

    public bool EsActivo { get; set; }

    public decimal Costo { get; set; }

    public decimal Factor { get; set; }

    public int? CategoriaId { get; set; }

    public decimal? PrecioVenta { get; set; }

    public virtual Categoria? Categoria { get; set; }

    public virtual ICollection<DetallesVentum> DetallesVenta { get; set; } = new List<DetallesVentum>();

    public virtual ICollection<InsumoProducto> InsumoProductos { get; set; } = new List<InsumoProducto>();
}
