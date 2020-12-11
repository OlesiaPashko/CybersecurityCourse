using BLL.DTOs;
using DLL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Services
{
    public interface IUserService
    {
        Task<bool> CreateUserAsync(UserDTO user);
        Task<bool> DeleteUserAsync(Guid id);
        Task<UserDTO> GetUserByIdAsync(Guid id);
        Task<List<UserDTO>> GetUsersAsync();
        Task<bool> UpdateUserAsync(UserDTO user);
    }
}
