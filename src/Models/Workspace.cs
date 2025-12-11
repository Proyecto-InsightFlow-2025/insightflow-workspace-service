using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace insightflow_workspace_service.src.Models
{
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