using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace insightflow_workspace_service.src.DTOs
{

    /// <summary>
    /// DTO para la edición de un espacio de trabajo.
    /// </summary>
    /// <remarks>
    /// Contiene los datos que se pueden modificar en un espacio de trabajo existente.
    /// </remarks>
    /// <example>
    /// {
    ///   "Name": "Updated Workspace Name",
    ///  "Description": "Updated Workspace Description",
    /// "ThematicArea": "Updated Workspace Thematic Area",
    /// "IconURL": [File]
    /// }
    /// </example>
    /// <returns>Datos del espacio de trabajo a editar.</returns>
    /// Se utiliza en el endpoint de edición de espacios de trabajo.
    public class EditWorkspaceDTO
    {
        public  string Name { get; set; } = "";
        public  string Description { get; set; } = "";
        public  string ThematicArea { get; set; } = "";
        public  IFormFile IconURL { get; set; } = null!;

    }
}