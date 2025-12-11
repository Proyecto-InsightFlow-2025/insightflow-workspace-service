using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using insightflow_workspace_service.src.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using insightflow_workspace_service.src.Helpers;

namespace insightflow_workspace_service.src.Controllers
{
    [Route("[controller]")]
    public class WorkspacesController : Controller
    {
        private readonly ApplicationDBContext _context;
        private readonly WorkspaceHelper _workspaceHelper;
        public WorkspacesController(ApplicationDBContext context)
        {
            _context = context;
            _workspaceHelper = new WorkspaceHelper(context);
        }

        [HttpPost]
        public IActionResult CreateWorkspace([FromBody] DTOs.CreateWorkspaceDTO dto)
        {
            if (dto == null)
            {
                var result = ResultHelper<Models.Workspace>.Fail("Invalid workspace data.", 400);
                return StatusCode(result.StatusCode, result);
            }
            
            if (!_workspaceHelper.IsWorkspaceNameUnique(dto.Name))
            {
                var result = ResultHelper<Models.Workspace>.Fail("Workspace name already exists.", 409);
                return StatusCode(result.StatusCode, result);
            }
            var workspace = new Models.Workspace
            {
                Id = _workspaceHelper.GenerateWorkspaceId(),
                Name = dto.Name,
                Description = dto.Description,
                ThematicArea = dto.ThematicArea,
                IconURL = dto.IconURL,
                OwnerId = dto.OwnerId,
                CreatedAt = DateTime.UtcNow,
                IsActive = true
            };

            _context.Workspaces.Add(workspace);
            
            var successResult = ResultHelper<Models.Workspace>.Success(workspace, message: "Workspace created successfully.");
            return StatusCode(successResult.StatusCode, successResult);
        }

        [HttpGet]
        public IActionResult GetAllWorkspaces()
        {
            var workspaces = _context.Workspaces;

            if (workspaces == null || !workspaces.Any())
            {
                var result = ResultHelper<IEnumerable<Models.Workspace>>.Fail("No workspaces found.", 404);
                return StatusCode(result.StatusCode, result);
            }

            var successResult = ResultHelper<IEnumerable<Models.Workspace>>.Success(workspaces, "Workspaces retrieved successfully.");
            return StatusCode(successResult.StatusCode, successResult);
        }

        [HttpGet("{id}")]
        public IActionResult GetWorkspaceById(Guid id)
        {
            var workspace = _context.Workspaces.FirstOrDefault(w => w.Id == id);

            if (workspace == null)
            {
                var result = ResultHelper<Models.Workspace>.Fail("Workspace not found.", 404);
                return StatusCode(result.StatusCode, result);
            }

            var successResult = ResultHelper<Models.Workspace>.Success(workspace, "Workspace retrieved successfully.");
            return StatusCode(successResult.StatusCode, successResult);
        }

        [HttpPatch("{id}")]

        public IActionResult EditWorkspace(Guid id, [FromBody] DTOs.EditWorkspaceDTO dto)
        {
            var workspace = _context.Workspaces.FirstOrDefault(w => w.Id == id);

            if (workspace == null)
            {
                var result = ResultHelper<Models.Workspace>.Fail("Workspace not found.", 404);
                return StatusCode(result.StatusCode, result);
            }

            workspace.Name = string.IsNullOrWhiteSpace(dto.Name) 
                ? workspace.Name 
                : dto.Name;

            workspace.Description = string.IsNullOrWhiteSpace(dto.Description) 
                ? workspace.Description 
                : dto.Description;

            workspace.ThematicArea = string.IsNullOrWhiteSpace(dto.ThematicArea) 
                ? workspace.ThematicArea 
                : dto.ThematicArea;

            workspace.IconURL = string.IsNullOrWhiteSpace(dto.IconURL) 
                ? workspace.IconURL 
                : dto.IconURL;


            var successResult = ResultHelper<Models.Workspace>.Success(workspace, "Workspace updated successfully.");
            return StatusCode(successResult.StatusCode, successResult);
     
        }
        
        [HttpDelete("{id}")]
        public IActionResult DeleteWorkspace(Guid id)
        {
            var workspace = _context.Workspaces.FirstOrDefault(w => w.Id == id);

            if (workspace == null)
            {
                var result = ResultHelper<Models.Workspace>.Fail("Workspace not found.", 404);
                return StatusCode(result.StatusCode, result);
            }

            workspace.IsActive = false;

            var successResult = ResultHelper<Models.Workspace>.Success(workspace, "Workspace deleted successfully.");
            return StatusCode(successResult.StatusCode, successResult);
        }
    }
}