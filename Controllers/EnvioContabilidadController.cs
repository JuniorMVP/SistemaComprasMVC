using Microsoft.AspNetCore.Mvc;
using SistemaComprasMVC.Models;
using SistemaComprasMVC.Services;
using SistemaComprasMVC.Data; // Asegúrate de incluir el espacio de nombres correcto para el contexto
using System;
using System.Collections.Generic;
using System.Linq; // Asegúrate de tener la directiva using para Linq

namespace SistemaComprasMVC.Controllers
{
    public class EnvioContabilidadController : Controller
    {
        private readonly IContabilidadService _contabilidadService;
        private readonly SistemaComprasContext _context;  // Usamos el contexto correcto

        public EnvioContabilidadController(IContabilidadService contabilidadService, SistemaComprasContext context)
        {
            _contabilidadService = contabilidadService;
            _context = context;  // Inyectamos el contexto correcto
        }

        // Acción para mostrar la vista de "Envío a Contabilidad"
        [HttpGet]
        public IActionResult Index()
        {
            return View("Index");
        }

        // Acción para filtrar las transacciones según las fechas
        [HttpPost]
        public IActionResult Filtrar(DateTime fechaDesde, DateTime fechaHasta)
        {
            // Lógica para filtrar las transacciones entre las fechas
            var transaccionesFiltradas = GetTransacciones(fechaDesde, fechaHasta);
            return View("Index", transaccionesFiltradas);  // Se vuelve a mostrar la vista con las transacciones filtradas
        }

        // Acción para enviar las transacciones seleccionadas a contabilidad
        [HttpPost]
        public IActionResult Contabilizar(List<int> transaccionIds)
        {
            try
            {
                // Obtener las transacciones seleccionadas por sus IDs
                var transacciones = GetTransaccionesPorIds(transaccionIds);

                // Llamar al servicio SOAP para enviar las transacciones
                var resultado = _contabilidadService.EnviarAsientos(transacciones);

                if (resultado)
                {
                    TempData["Message"] = "Las transacciones han sido enviadas a contabilidad exitosamente.";
                }
                else
                {
                    TempData["Message"] = "Hubo un error al enviar las transacciones.";
                }

                return RedirectToAction("Index");  // Regresa a la vista principal
            }
            catch (Exception ex)
            {
                TempData["Message"] = "Error: " + ex.Message;
                return RedirectToAction("Index");
            }
        }

        // Método para obtener las transacciones entre las fechas dadas
        private List<Transaccion> GetTransacciones(DateTime fechaDesde, DateTime fechaHasta)
        {
            // Sumar el CostoUnitario de todas las órdenes de compra en el rango de fechas especificado
            decimal montoTotal = _context.OrdenesDeCompra
                .Where(oc => oc.FechaOrden >= fechaDesde && oc.FechaOrden <= fechaHasta)
                .Sum(oc => oc.CostoUnitario);


            // Aquí podemos crear una descripción dinámica basada en las fechas proporcionadas
            string descripcion = $"Asiento de Compras correspondiente al periodo {fechaDesde:yyyy-MM}";

            // Crear una lista con una transacción que representa el total
            return new List<Transaccion>
            {
                new Transaccion
                {
                    IdTransaccion = 1,  // Puedes ajustar este ID según tu lógica
                    Descripcion = descripcion, // Descripción dinámica basada en la fecha
                    FechaTransaccion = DateTime.Now,
                    Monto = montoTotal,  // Usamos el monto total calculado
                    IdAsiento = null,    // Esto se llenará después de contabilizar
                    IdAuxiliar = 7,      // Valor fijo para IdAuxiliar
                    CuentaDB = 80,       // Valor fijo para CuentaDB (Compra de Mercancias)
                    CuentaCR = 4         // Valor fijo para CuentaCR (Cuenta Corriente Banco)
                }
            };
        }

        // Método para obtener las transacciones por sus IDs
        private List<Transaccion> GetTransaccionesPorIds(List<int> transaccionIds)
        {
            var todasLasTransacciones = GetTransacciones(DateTime.MinValue, DateTime.MaxValue);

            // Filtrar las transacciones que coincidan con los IDs seleccionados
            return todasLasTransacciones.Where(t => transaccionIds.Contains(t.IdTransaccion)).ToList();
        }
    }
}
