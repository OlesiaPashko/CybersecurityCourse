using BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public interface IUserProfileService
    {
        Task<UserDTO> GetUserProfile(string id);
    }
}
