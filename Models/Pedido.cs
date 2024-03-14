using System;
using System.Collections.Generic;

namespace FloreriaMvc.Models;

public partial class Pedido
{
    public int PedidoId { get; set; }

    public DateOnly? DiaEntrega { get; set; }

    public DateTime? HoraEntrega { get; set; }

    public string? Detalles { get; set; }

    public string? Estado { get; set; }

    public int? ClienteId { get; set; }

    public virtual Cliente? Cliente { get; set; }
}
