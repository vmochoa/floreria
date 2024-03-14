using System;
using System.Collections.Generic;

namespace FloreriaMvc.Models;

public partial class Ventum
{
    public int VentaId { get; set; }

    public DateOnly? FechaVenta { get; set; }

    public decimal? TotalVenta { get; set; }

    public int? ClienteId { get; set; }

    public virtual Cliente? Cliente { get; set; }

    public virtual ICollection<DetallesVentum> DetallesVenta { get; set; } = new List<DetallesVentum>();
}
