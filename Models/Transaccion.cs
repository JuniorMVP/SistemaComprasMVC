namespace SistemaComprasMVC.Models
{
    public class Transaccion
    {
        public int IdTransaccion { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaTransaccion { get; set; }
        public decimal Monto { get; set; }
        public int? IdAsiento { get; set; } // Puede ser null si aún no se ha asignado un asiento contable
        public int IdAuxiliar { get; set; }  // Asegúrate de definir esta propiedad
        public int CuentaDB { get; set; }  // Asegúrate de definir esta propiedad
        public int CuentaCR { get; set; }  // Asegúrate de definir esta propiedad
    }
}
