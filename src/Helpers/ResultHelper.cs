using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace insightflow_workspace_service.src.Helpers
{
    /// <summary>
    /// Clase genérica para manejar resultados de operaciones.
    /// </summary>
    /// <typeparam name="T">Tipo de dato que se maneja en el resultado.</typeparam>
    /// <remarks>
    /// Proporciona una estructura para devolver resultados exitosos o fallidos de operaciones, incluyendo mensajes y códigos de estado.
    /// </remarks>  
    /// 
    public class ResultHelper<T>
    {
        public bool IsSuccess { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }

        internal static ResultHelper<T> Fail(string v, int statusCode = 400)
        {
            return new ResultHelper<T> { IsSuccess = false, Message = v, StatusCode = statusCode };
        }

        internal static ResultHelper<T> Success(T data, string message = "")
        {
            return new ResultHelper<T> { IsSuccess = true, Data = data, StatusCode = 200, Message = message };
        }
    }
}