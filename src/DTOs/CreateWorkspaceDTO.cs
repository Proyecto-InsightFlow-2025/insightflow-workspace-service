using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace insightflow_workspace_service.src.DTOs
{
    public class CreateWorkspaceDTO
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required string ThematicArea { get; set; }
        public required IFormFile IconURL { get; set; }
        public required Guid OwnerId { get; set; }
    }
}