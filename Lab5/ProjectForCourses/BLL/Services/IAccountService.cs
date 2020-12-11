using BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Services
{
    public interface IAccountService
    {
        Task<AuthentificationResultDTO> RegisterAsync(UserDTO userDTO);
        Task<AuthentificationResultDTO> LoginAsync(LoginDTO loginDTO);

    }
}
