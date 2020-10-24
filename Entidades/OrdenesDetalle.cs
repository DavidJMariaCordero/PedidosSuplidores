using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PedidosSuplidores.Entidades
{
    public class OrdenesDetalle
    {

        [Key]
        public int Id { get; set; }
        public int OrdenId { get; set; }
        public int ProductoId { get; set; }
        public float Cantidad { get; set; }
        public decimal Costo { get; set; }
        public OrdenesDetalle(int ordenId, int productoId, float cantidad, decimal costo)
         {
             this.OrdenId = ordenId;
             this.ProductoId = productoId;
             this.Cantidad = cantidad;
             this.Costo = costo;
         }
        public OrdenesDetalle(int orden, int producto)
        {
            this.OrdenId = orden;
            this.ProductoId = producto;
        }
           


    }
}
