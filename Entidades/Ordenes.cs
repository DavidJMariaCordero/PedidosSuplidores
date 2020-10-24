using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PedidosSuplidores.Entidades
{
    public class Ordenes
    {
        [Key]
        public int OrdenId { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Monto { get; set; }
        public int SuplidorId { get; set; }

        [ForeignKey("OrdenId")]

        public virtual List<OrdenesDetalle> Detalle { get; set; } = new List<OrdenesDetalle>();
    }
}
