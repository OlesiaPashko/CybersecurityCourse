using BLL.DTOs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public interface IImageService
    {
        Task<bool> UploadImage(string userId, IFormFile file, ImageDTO imageDTO);

        Task<GetImageDTO> GetImageAsync(string userId, Guid imageId, string rootPath);


        Task<GetImageDTO> GetAllImagesAsZipAsync(string userId, string rootPath);

        Task DeleteImageAsync(string userId, Guid imageId, string rootPath);

    }
}
