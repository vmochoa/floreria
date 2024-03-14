using System;
using System.Collections.Generic;

namespace FloreriaMvc.Models;

public partial class DetallesVentum
{
    public int DetallesVentaId { get; set; }

    public int? VentaId { get; set; }

    public int? ProductoFinalId { get; set; }

    public int? Cantidad { get; set; }

    public virtual ProductosFinale? ProductoFinal { get; set; }

    public virtual Ventum? Venta { get; set; }
}
