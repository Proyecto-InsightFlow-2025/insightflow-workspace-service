using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using insightflow_workspace_service.src.Models;

namespace insightflow_workspace_service.src.Data
{
    // Simulación de un contexto de base de datos para la aplicación.
    // Proporciona acceso a las entidades de la base de datos, como los espacios de trabajo.
    // Se utiliza para interactuar con la base de datos en los controladores y servicios.
    public class ApplicationDBContext
    {
        public List<Workspace> Workspaces { get; set; } = new List<Workspace>();
    }
}