using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace insightflow_workspace_service.src.Models
{
    /// <summary>
    /// Modelo que representa un espacio de trabajo.
    /// </summary>
    /// <remarks>
    /// Contiene las propiedades y datos asociados a un espacio de trabajo dentro de la aplicación.
    /// </remarks>
    /// <example>
    /// {
    ///   "Id": "Guid",
    ///  "Name": "Workspace Name",
    /// "Description": "Workspace Description",
    /// "ThematicArea": "Workspace Thematic Area",
    /// "IconURL": "http://example.com/icon.png",
    /// "CreatedAt": "2023-01-01T00:00:00Z",
    /// "OwnerId": "Guid of the owner",
    /// "MemberIds": ["Guid1", "Guid2"],
    /// "IsActive": true
    /// }
    /// </example>
    /// <returns>Datos del espacio de trabajo.</returns>
    /// Se utiliza en la gestión y manipulación de espacios de trabajo en la aplicación.
    public class Workspace
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public string ThematicArea { get; set; } = "";
        public string IconURL { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public Guid OwnerId { get; set; }
        public IEnumerable<Guid> MemberIds { get; set; } = new List<Guid>();
        public bool IsActive { get; set; } = true;
    }
}