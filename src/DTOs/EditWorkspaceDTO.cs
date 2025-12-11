using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace insightflow_workspace_service.src.DTOs
{
    public class EditWorkspaceDTO
    {
        public  string Name { get; set; } = "";
        public  string Description { get; set; } = "";
        public  string ThematicArea { get; set; } = "";
        public  IFormFile IconURL { get; set; } = null!;

    }
}