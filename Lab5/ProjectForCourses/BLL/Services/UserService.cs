using AutoMapper;
using BLL.DTOs;
using DLL;
using DLL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class UserService:IUserService
    {
        private IUnitOfWork _unitOfWork { get; set; }
        private IMapper _mapper { get; set; }
        UserManager<User> _userManager { get; set; }
        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        private User UserDTOToUser(UserDTO userDTO)
        {
            return new User
            {
                FirstName = userDTO.FirstName,
                LastName = userDTO.LastName,
                UserName = userDTO.UserName,
                Id = userDTO.Id.ToString(),
                Email = userDTO.Email
            };
        }

        private UserDTO UserToUserDTO(User user)
        {
            return new UserDTO
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Id = Guid.Parse(user.Id),
                Email = user.Email
            };
        }
        public async Task<bool> CreateUserAsync(UserDTO userDTO)
        {
            User user = UserDTOToUser(userDTO);
            await _userManager.CreateAsync(user, userDTO.Password);
            var createdCount = await _unitOfWork.CommitAsync();
            return createdCount > 0;
        }

        public async Task<bool> DeleteUserAsync(Guid id)
        {
            var user = await GetUserByIdAsync(id);
            _unitOfWork.Users.Remove(UserDTOToUser(user));
            var deletedCount = await _unitOfWork.CommitAsync();
            return deletedCount > 0;
        }

        public async Task<UserDTO> GetUserByIdAsync(Guid id)
        {
            return UserToUserDTO(await _unitOfWork.Users.SingleOrDefaultAsync(x => x.Id == id.ToString()));
        }

        public async Task<List<UserDTO>> GetUsersAsync()
        {
            var users = await _unitOfWork.Users.GetAllAsync();
            var userDTOs = new List<UserDTO>();
            foreach(var user in users)
            {
                userDTOs.Add(UserToUserDTO(user));
            }
            return userDTOs;
        }

        public async Task<bool> UpdateUserAsync(UserDTO user)
        {
            var userToUpdate = await GetUserByIdAsync(user.Id);
            userToUpdate.Email = user.Email;
            userToUpdate.FirstName = user.FirstName;
            userToUpdate.LastName = user.LastName;
            userToUpdate.UserName = user.UserName;
            var updatedCount = await _unitOfWork.CommitAsync();
            return updatedCount > 0;
        }
    }

}
