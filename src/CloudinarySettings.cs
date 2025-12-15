using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace insightflow_workspace_service.src
{

    /// <summary>
    /// Configuración para la integración con Cloudinary.
    /// </summary>
    /// <remarks>
    /// Contiene las credenciales necesarias para conectarse a la cuenta de Cloudinary.
    /// </remarks>
    /// <example>
    /// {
    ///   "CloudName": "your_cloud_name",
    ///  "ApiKey": "your_api_key",
    /// "ApiSecret": "your_api_secret"
    /// }
    public class CloudinarySettings
    {
            
        public string CloudName { get; set; }  = null!;
        public string ApiKey { get; set; } = null!;
        public string ApiSecret { get; set; } = null!;
    }
}