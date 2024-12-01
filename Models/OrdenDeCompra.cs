using System;
using System.ComponentModel.DataAnnotations;

namespace SistemaComprasMVC.Models
{
    public class OrdenDeCompra
    {
        public int Id { get; set; }  // ID único de la orden

        [Required]
        public string NumeroOrden { get; set; }  // Número de la Orden

        [Required]
        public DateTime FechaOrden { get; set; }  // Fecha de la Orden

        [Required]
        public bool Estado { get; set; }  // Estado de la Orden

        // Relación con Articulo
        public int ArticuloId { get; set; }
        public Articulo Articulo { get; set; }

        // Relación con UnidadDeMedida
        public int UnidadDeMedidaId { get; set; }
        public UnidadDeMedida UnidadDeMedida { get; set; }

        [Required]
        public int Cantidad { get; set; }  // Cantidad solicitada

        [Required]
        public decimal CostoUnitario { get; set; }  // Costo unitario
    }
}
