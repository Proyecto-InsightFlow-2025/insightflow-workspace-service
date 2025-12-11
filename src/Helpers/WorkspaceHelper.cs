using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using insightflow_workspace_service.src.Data;


namespace insightflow_workspace_service.src.Helpers
{
    public class WorkspaceHelper
    {
        private readonly ApplicationDBContext _context; 

        public WorkspaceHelper(ApplicationDBContext context)
        {
            _context = context;
        }

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

        public bool IsWorkspaceNameUnique(string name)
        {
            return !_context.Workspaces.Any(w => w.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }
    }
}