using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using insightflow_workspace_service.src.Models;

namespace insightflow_workspace_service.src.Data
{
    public class ApplicationDBContext
    {
        public List<Workspace> Workspaces { get; set; } = new List<Workspace>();
    }
}