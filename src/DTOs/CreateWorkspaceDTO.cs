using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace insightflow_workspace_service.src.DTOs
{
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