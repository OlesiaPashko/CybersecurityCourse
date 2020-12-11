using AutoMapper;
using BLL.DTOs;
using DLL;
using DLL.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ImageService:IImageService
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public ImageService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ImageDTO>> getAllAsync()
        {
            return _mapper.Map< List<Image> , List<ImageDTO>>(await _unitOfWork.Images.GetAllAsync());   
        }

        /*public File getFIleAsync(string path)
        {
            byte[] mas = File.ReadAllBytes(path);
            string extention = Path.GetExtension(path);
            string type = "png";
            if (extention == "jpg")
                type = "jpeg";
            string file_type = "application/" + type;
            // Имя файла - необязательно
            string file_name = Path.GetFileName(path);
            return File(mas, file_type, file_name);
        }*/
        public async Task<bool> UploadImage(string userId, IFormFile file, ImageDTO imageDTO)
        {
            var user = await _unitOfWork.Users.SingleOrDefaultAsync(u => u.Id == userId);
            var login = user.UserName;
            var rootPath = imageDTO.Path;
            if (!Directory.Exists(rootPath + @"\Images\" + login))
            {
                Directory.CreateDirectory(rootPath + @"\Images\" + login);
            }
            //Upload Image
            var postedFile = file;
            //Create custom filename
            var imageName = new String(Path.GetFileNameWithoutExtension(postedFile.FileName).Take(10).ToArray()).Replace(" ", "-");
            imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(postedFile.FileName);
            string path = Path.Combine("Images", login, imageName);
            using (var fileStream = new FileStream(rootPath + path, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
            Image image = new Image { Path = path, Caption = imageDTO.Caption, Id = new Guid()};
            await _unitOfWork.Images.AddAsync(image);
            await _unitOfWork.LikesFromUserToImage.AddAsync(new UserImage { Image = image, User = user });
            return (await _unitOfWork.CommitAsync()>0);
        }

        public async Task<GetImageDTO> GetImageAsync(string userId, Guid imageId, string rootPath)
        {
            var image = await _unitOfWork.Images.SingleOrDefaultAsync(im => im.Id == imageId);
            var user = await _unitOfWork.Users.SingleOrDefaultAsync(u => u.Id == userId);
            var pathToImage = "";
            try
            {
                pathToImage = GetPathToImage(user, image, rootPath);
            }
            catch(Exception ex)
            {
                return new GetImageDTO { Error = ex.Message, Success = false };
            }

            return new GetImageDTO { Success = true, FileStream = File.OpenRead(pathToImage), Caption= image.Caption };
        }

        public async Task<GetImageDTO> GetAllImagesAsZipAsync(string userId, string rootPath)
        {
            var user = await _unitOfWork.Users.SingleOrDefaultAsync(u => u.Id == userId);
            if (user == null)
                return new GetImageDTO { Success = false, Error = "there is no such user" };
            var pathToDir = Path.Combine(rootPath , "Images", user.UserName);
            if (!Directory.Exists(pathToDir))
            {
                return new GetImageDTO { Success = false, Error = "There is no directory for this user" };
            }
            var pathToZip = Path.Combine(rootPath, user.UserName + ".zip");
            ZipFile.CreateFromDirectory(pathToDir, pathToZip);
            return new GetImageDTO { Success=true, FileStream=File.OpenRead(pathToZip)};
        }

        public async Task DeleteImageAsync(string userId, Guid imageId, string rootPath)
        {
            var image = await _unitOfWork.Images.SingleOrDefaultAsync(im => im.Id == imageId);
            var user = await _unitOfWork.Users.SingleOrDefaultAsync(u => u.Id == userId);
            var pathToImage = "";
            try
            {
                pathToImage = GetPathToImage(user, image, rootPath);
                File.Delete(pathToImage);
                _unitOfWork.Images.Remove(image);
                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string GetPathToImage(User user, Image image, string rootPath)
        {
            if (image == null)
                throw new Exception("Image id is wrong");
            if(user == null)
                throw new Exception("There is no such user in the system");
            if(!UserOwnsImage(image, rootPath, user))
                throw new Exception("it`s not your photo");
            var pathToImage = rootPath + image.Path;
            if (!IsPathValid(pathToImage))
                throw new Exception("Image doesn`t exist");
            return pathToImage;
        }

        private bool UserOwnsImage(Image image, string rootPath, User user)
        {
            var login = user.UserName;
            var pathToImageFromDb = rootPath + image.Path;
            var pathToDirectoryForCurrentUser = rootPath + @"\Images\" + login;
            var pathToDirectoryFromDb = GetPathToDirectory(pathToImageFromDb);
            if (!pathToDirectoryForCurrentUser.Equals(pathToDirectoryFromDb))
            {
                return false;
            }
            return true;
        }

        private string GetPathToDirectory(string pathToFile)
        {
            return pathToFile.Substring(0, pathToFile.LastIndexOf('\\'));
        }
        private bool IsPathValid(string pathToImage)
        {
            var pathToDirectory = GetPathToDirectory(pathToImage);
            if (!Directory.Exists(pathToDirectory) || !File.Exists(pathToImage))
            {
                return false;
            }
            return true;
        }

    }
}
