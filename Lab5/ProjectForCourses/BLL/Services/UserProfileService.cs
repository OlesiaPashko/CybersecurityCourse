using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BLL.DTOs;
using DLL;
using DLL.Entities;
using Microsoft.AspNetCore.Identity;

namespace BLL.Services
{
    public class UserProfileService : IUserProfileService
    {
        private IUnitOfWork _unitOfWork { get; set; }
        private UserManager<User> _userManager { get; set; }
        public UserProfileService(IUnitOfWork unitOfWork, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }
        public async Task<UserDTO> GetUserProfile(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            return new UserDTO
            {
                UserName = user.UserName,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName
            };
        }
    }
}
