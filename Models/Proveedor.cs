using System.ComponentModel.DataAnnotations;

namespace SistemaComprasMVC.Models
{
    public class Proveedor
    {
        public int Id { get; set; } // Identificador

        [Required(ErrorMessage = "La cédula es obligatoria.")]
        [RegularExpression(@"^[0-9]{11}$", ErrorMessage = "Formato de cédula no válido.")]
        public string CedulaORNC { get; set; } // Cédula

        [Required(ErrorMessage = "El nombre comercial es obligatorio.")]
        public string NombreComercial { get; set; } // Nombre del proveedor

        public bool Estado { get; set; } // Estado (activo/inactivo)

        // Método de validación de cédula
        public static bool ValidaCedula(string pCedula)
        {
            int vnTotal = 0;
            string vcCedula = pCedula.Replace("-", "");
            int pLongCed = vcCedula.Trim().Length;
            int[] digitoMult = new int[11] { 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1 };

            if (pLongCed != 11)
                return false;

            for (int vDig = 1; vDig <= pLongCed; vDig++)
            {
                int vCalculo = Int32.Parse(vcCedula.Substring(vDig - 1, 1)) * digitoMult[vDig - 1];
                if (vCalculo < 10)
                    vnTotal += vCalculo;
                else
                    vnTotal += Int32.Parse(vCalculo.ToString().Substring(0, 1)) + Int32.Parse(vCalculo.ToString().Substring(1, 1));
            }
            return vnTotal % 10 == 0;
        }
    }
}
