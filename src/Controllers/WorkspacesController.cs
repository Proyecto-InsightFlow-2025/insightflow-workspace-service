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

    // Este controller gestiona las operaciones CRUD para los espacios de trabajo. proporciona endpoints para crear, leer, actualizar y eliminar espacios de trabajo.
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
    
        //<Summary>
        // Crea un nuevo espacio de trabajo.
        //</Summary>
        /// <param name="dto">
        /// Datos del espacio de trabajo a crear.
        /// </param>
        /// <returns>Resultado de la operación de creación.</returns>

        [HttpPost]
        public IActionResult CreateWorkspace([FromForm] DTOs.CreateWorkspaceDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
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
            String url = "";
            try
            {
                url = _cloudinaryService.UploadImageAsync(dto.IconURL).Result;
            }
            catch (Exception ex)
            {
                var result = ResultHelper<Models.Workspace>.Fail($"Image upload failed: {ex.Message}", 400);
                return StatusCode(result.StatusCode, result);
            }

                    

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

        // <Summary>
        // Obtiene todos los espacios de trabajo de un propietario específico.
        // </Summary>
        /// <param name="ownerId">ID del propietario cuyos espacios de trabajo se desean obtener
        /// </param>
        /// <returns>Lista de espacios de trabajo del propietario.</returns>
        
        [HttpGet]
        public IActionResult GetAllWorkspacesByOwner([FromQuery]Guid ownerId)
        {
            var workspaces = _context.Workspaces.Where(w => w.OwnerId == ownerId).ToList();

            if (workspaces == null || !workspaces.Any())
            {
                var result = ResultHelper<IEnumerable<Models.Workspace>>.Fail("No workspaces found.", 404);
                return StatusCode(result.StatusCode, result);
            }

            var successResult = ResultHelper<IEnumerable<Models.Workspace>>.Success(workspaces, "Workspaces retrieved successfully.");
            return StatusCode(successResult.StatusCode, successResult);
        }

        // <Summary>
        // Obtiene un espacio de trabajo por su ID.
        // </Summary>
        /// <param name="id">ID del espacio de trabajo a obtener.
        /// </param>
        /// <returns>Espacio de trabajo solicitado.</returns>
        /// 
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

        // <Summary>
        // Edita un espacio de trabajo existente.
        // </Summary>
        /// <param name="id">ID del espacio de trabajo a editar.
        /// </param>
        /// <param name="dto">Datos del espacio de trabajo a editar.
        /// </param>
        /// <returns>Resultado de la operación de edición.</returns>
        ///   
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
                String url = "";
                try
                {
                    url = _cloudinaryService.UploadImageAsync(dto.IconURL).Result;
                }
                catch (Exception ex)
                {
                    var result = ResultHelper<Models.Workspace>.Fail($"Image upload failed: {ex.Message}", 400);
                    return StatusCode(result.StatusCode, result);
                }      
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
        // <Summary>
        // Elimina un espacio de trabajo por su ID.
        // </Summary>
        /// <param name="id">ID del espacio de trabajo a eliminar (Soft delete). 
        /// </param>
        /// <returns>Resultado de la operación de eliminación.</returns>
        /// 
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