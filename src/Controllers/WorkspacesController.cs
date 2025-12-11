using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using insightflow_workspace_service.src.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using insightflow_workspace_service.src.Helpers;
using insightflow_workspace_service.src.Service;

namespace insightflow_workspace_service.src.Controllers
{
    [Route("[controller]")]
    public class WorkspacesController : Controller
    {
        private readonly ApplicationDBContext _context;
        private readonly WorkspaceHelper _workspaceHelper;
        private readonly CloudinaryService _cloudinaryService;
        public WorkspacesController(ApplicationDBContext context, CloudinaryService cloudinaryService)
        {
            _context = context;
            _workspaceHelper = new WorkspaceHelper(context);
            _cloudinaryService = cloudinaryService;
        }

        [HttpPost]
        public IActionResult CreateWorkspace([FromForm] DTOs.CreateWorkspaceDTO dto)
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

            String url = _cloudinaryService.UploadImageAsync(dto.IconURL).Result;        

            var workspace = new Models.Workspace
            {
                Id = _workspaceHelper.GenerateWorkspaceId(),
                Name = dto.Name,
                Description = dto.Description,
                ThematicArea = dto.ThematicArea,
                IconURL = url,
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

        public IActionResult EditWorkspace(Guid id, [FromForm] DTOs.EditWorkspaceDTO dto)
        {
            var workspace = _context.Workspaces.FirstOrDefault(w => w.Id == id);

            if (workspace == null)
            {
                var result = ResultHelper<Models.Workspace>.Fail("Workspace not found.", 404);
                return StatusCode(result.StatusCode, result);
            }

              if (dto.IconURL != null)
            {
                String url = _cloudinaryService.UploadImageAsync(dto.IconURL).Result;        
                workspace.IconURL = url;
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