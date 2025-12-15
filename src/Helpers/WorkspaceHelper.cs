using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using insightflow_workspace_service.src.Data;


namespace insightflow_workspace_service.src.Helpers
{
    /// <summary>
    /// Clase auxiliar para operaciones relacionadas con espacios de trabajo.
    /// </summary>
    /// <remarks>
    /// Proporciona métodos para gestionar y validar espacios de trabajo dentro de la aplicación.
    /// </remarks>
    /// <example>
    /// var workspaceHelper = new WorkspaceHelper(context);
    /// var isUnique = workspaceHelper.IsWorkspaceNameUnique("New Workspace");
    /// </example>
    
    public class WorkspaceHelper
    {
        private readonly ApplicationDBContext _context; 

        public WorkspaceHelper(ApplicationDBContext context)
        {
            _context = context;
        }

        // Genera un nuevo ID único para un espacio de trabajo.
        public Guid GenerateWorkspaceId()
        {
            Guid newId;
            do
            {
                newId = Guid.NewGuid();
            }
            while (_context.Workspaces.Any(w => w.Id == newId));

            return newId;
        }
        // Verifica si el nombre del espacio de trabajo es único.
        public bool IsWorkspaceNameUnique(string name)
        {
            return !_context.Workspaces.Any(w => w.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }
    }
}