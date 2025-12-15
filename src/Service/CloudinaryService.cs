using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace insightflow_workspace_service.src.Service
{
    /// <summary>
    /// Servicio para manejar la carga de imágenes a Cloudinary.
    /// </summary>
    /// <remarks>
    /// Proporciona métodos para subir imágenes y gestionar la configuración de Cloudinary.
    /// </remarks>
    /// <example>
    /// var cloudinaryService = new CloudinaryService(cloudinary);
    /// var imageUrl = await cloudinaryService.UploadImageAsync(file);
    /// </example>
    /// 
    public class CloudinaryService
    {
        private readonly Cloudinary _cloudinary;

        public CloudinaryService(Cloudinary cloudinary)
        {
            _cloudinary = cloudinary;
        }

        public async Task<string> UploadImageAsync(IFormFile file)
        {
            if (file.Length == 0)
                throw new Exception("Archivo vacío");

            await using var stream = file.OpenReadStream();

            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(file.FileName, stream),
                Folder = "workspaces"        
            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);

            if (uploadResult.Error != null)
                throw new Exception(uploadResult.Error.Message);

            return uploadResult.SecureUrl.ToString();
        }
    }
}