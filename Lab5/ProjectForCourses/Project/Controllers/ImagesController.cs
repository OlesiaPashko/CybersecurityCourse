using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Extentions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using BLL.Services;
using BLL.DTOs;
using Project.Contracts.V1.Responses;

namespace Project.Controllers.V1
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ImagesController : Controller
    {

        private readonly IImageService _imageService;
        IHostingEnvironment _appEnvironment;
        private IUserService _userService;

        public ImagesController(IImageService imageService, IHostingEnvironment hostingEnvironment, IUserService userService)
        {
            _imageService = imageService;
            _appEnvironment = hostingEnvironment;
            _userService = userService;
        }

        [HttpPost, DisableRequestSizeLimit]
        [Route("api/UploadImage")]
        public async Task<IActionResult> UploadImage()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var file = Request.Form.Files.GetFile("Image");
            var caption = String.Format("{0}", Request.Form["ImageCaption"]);
            var userId = HttpContext.GetUserId();
            bool success = await _imageService.UploadImage(userId, file, new ImageDTO { Path = _appEnvironment.WebRootPath, Caption = caption });
            if(success)
            {
                return Ok("image was added");
            }
            else
            {
                return BadRequest("Image can not be added");
            }
        }

        [HttpGet("api/images/{id}")]
        public async Task<ActionResult<GetImageResponse>> Get(Guid id)
        {
            var userId = HttpContext.GetUserId();
            var rootPath = _appEnvironment.WebRootPath;
            var getImageDTO = await _imageService.GetImageAsync(userId, id, rootPath);
            if (!getImageDTO.Success)
            {
                if(getImageDTO.Error== "it`s not your photo")
                {
                    return BadRequest(getImageDTO.Error);
                }
                return NotFound(getImageDTO.Error);
            }
            return Ok(new GetImageResponse {File = getImageDTO.FileStream, Caption = getImageDTO.Caption});
        }

        [HttpGet("api/images/zip")]
        public async Task<IActionResult> GetAllZip()
        {
            var userId = HttpContext.GetUserId();
            var rootPath = _appEnvironment.WebRootPath;
            var zipDTO = await _imageService.GetAllImagesAsZipAsync(userId, rootPath);
            if (zipDTO.Success == false)
            {
                if (zipDTO.Error == "There is no directory for this user")
                    return NoContent();
                return BadRequest(zipDTO.Error);
            }
            return File(zipDTO.FileStream, "application/zip", "Image.zip");
        }

        [HttpDelete("api/images/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var userId = HttpContext.GetUserId();
            var rootPath = _appEnvironment.WebRootPath;
            try
            {
                await _imageService.DeleteImageAsync(userId, id, rootPath);
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
            return Ok("Deleted successfully");
        }
    }
}