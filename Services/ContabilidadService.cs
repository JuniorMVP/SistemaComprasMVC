using System;
using System.Collections.Generic;
using System.ServiceModel;
using ContabilidadServiceReference;
using SistemaComprasMVC.Models;
using Microsoft.Extensions.Logging; // Usando ILogger para mejor control de logs

namespace SistemaComprasMVC.Services
{
    public interface IContabilidadService
    {
        bool EnviarAsientos(List<Transaccion> transacciones);
    }

    public class ContabilidadService : IContabilidadService
    {
        private readonly AsientoContableServiceSoapClient _client;
        private readonly ILogger<ContabilidadService> _logger; // Logger para gestionar los logs

        public ContabilidadService(ILogger<ContabilidadService> logger)
        {
            // Inicializamos el cliente SOAP con la configuración que se generó automáticamente
            _client = new AsientoContableServiceSoapClient(AsientoContableServiceSoapClient.EndpointConfiguration.AsientoContableServiceSoap12);
            _logger = logger; // Inyectamos el logger
        }

        public bool EnviarAsientos(List<Transaccion> transacciones)
        {
            try
            {
                foreach (var transaccion in transacciones)
                {
                    // Aquí se envía cada transacción al servicio SOAP
                    _logger.LogInformation($"Enviando asiento: {transaccion.Descripcion}");

                    // Lógica de llamada al método SOAP 'RegistrarAsiento'
                    var resultado = _client.RegistrarAsiento(
                        transaccion.IdAuxiliar,
                        transaccion.Descripcion,
                        transaccion.CuentaDB,
                        transaccion.CuentaCR,
                        transaccion.Monto
                    );

                    // Supongamos que el resultado no es nulo si la operación fue exitosa
                    if (resultado == null)
                    {
                        _logger.LogError("Error al registrar el asiento: El servicio devolvió un resultado nulo.");
                        return false; // Si una transacción falla, detenemos el proceso
                    }
                }

                // Si todas las transacciones fueron enviadas correctamente
                return true;
            }
            catch (Exception ex)
            {
                // Capturamos y mostramos cualquier error que ocurra durante la llamada al servicio
                _logger.LogError($"Error al enviar los asientos: {ex.Message}");
                return false;
            }
        }
    }
}
