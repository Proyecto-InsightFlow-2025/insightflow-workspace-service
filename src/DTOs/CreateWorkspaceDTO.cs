using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace insightflow_workspace_service.src.DTOs
{
    /// <summary>
    /// DTO para la creación de un espacio de trabajo.
    /// </summary>
    /// <remarks>
    /// Contiene los datos necesarios para crear un nuevo espacio de trabajo.
    /// </remarks>
    /// <example>
    /// {
    ///    "Name": "Workspace Name",
    ///   "Description": "Workspace Description",
    ///  "ThematicArea": "Workspace Thematic Area",
    ///  "IconURL": [File],
    /// "OwnerId": "Guid of the owner"
    /// }
    /// </example>
    /// <returns>Datos del espacio de trabajo a crear.</returns>
    /// Se utiliza en el endpoint de creación de espacios de trabajo.
    /// 
    public class CreateWorkspaceDTO
    {
        [Required]
        public required string Name { get; set; }
        [Required]
        public required string Description { get; set; }
        [Required]
        public required string ThematicArea { get; set; }
        [Required]
        public required IFormFile IconURL { get; set; }
        [Required]
        public required Guid OwnerId { get; set; }
    }
}