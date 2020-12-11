using DLL;
using DLL.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Options;
using BLL.DTOs;
using AutoMapper;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace BLL.Services
{
    public class AccountService:IAccountService
    {
        private UserManager<User> _userManager { get; set; }
        private RoleManager<IdentityRole> _roleManager { get; set; }
        private IUnitOfWork _unitOfWork { get; set; }
        private IMapper _mapper { get; set; }

        private JwtSettings _jwtSettings { get; set; }

        private async Task<string> GetTokenAsync(User userIdentity)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
            var userRoles = await _userManager.GetRolesAsync(userIdentity);
            string role = "User";
            if (userRoles.Count > 0) {
                role = userRoles[0];
            }
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                    new Claim(JwtRegisteredClaimNames.Sub, userIdentity.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, userIdentity.Email),
                    new Claim("id", userIdentity.Id),
                    new Claim(ClaimTypes.Role, role)
                }),
                    Expires = DateTime.UtcNow.AddHours(5),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                }; 
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        

        public AccountService(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IUnitOfWork unitOfWork, 
            JwtSettings jwtSettings, IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _jwtSettings = jwtSettings;
        }

        private async Task CreateRoles()
        {
            if (!await _roleManager.RoleExistsAsync("Admin"))
            {
                var admin = new IdentityRole("Admin");
                await _roleManager.CreateAsync(admin);
            }
            if (!await _roleManager.RoleExistsAsync("User"))
            {
                var user = new IdentityRole("User");
                await _roleManager.CreateAsync(user);
            }
        }

        public async Task<AuthentificationResultDTO> RegisterAsync(UserDTO userDTO)
        {
            await CreateRoles();
            if (_unitOfWork.Users.Find(elem => elem.UserName == userDTO.UserName).Count(x=>x==x) > 0)
                return new AuthentificationResultDTO
                {
                    Token = "",
                    Errors = new IdentityError[] { new IdentityError() { Code = "DuplicateUserName", Description = "Username is already taken" } },
                    Success = false
                };
            User userIdentity = new User { Email=userDTO.Email, FirstName = userDTO.FirstName, LastName = userDTO.LastName, UserName = userDTO.UserName};
            
            var result = await _userManager.CreateAsync(userIdentity, userDTO.Password);
            if (!result.Succeeded)
                return new AuthentificationResultDTO
                {
                    Token = "",
                    Errors = result.Errors,
                    Success = false
                };
            await _unitOfWork.CommitAsync();
            string token = await GetTokenAsync(userIdentity);
            return new AuthentificationResultDTO { Token = token, Errors = result.Errors, Success = result.Succeeded };
        }

        public async Task<AuthentificationResultDTO> LoginAsync(LoginDTO loginDTO)
        {
            var user = await _userManager.FindByNameAsync(loginDTO.UserName);

            if (user == null)
                return new AuthentificationResultDTO
                {
                    Errors = new List<IdentityError>{ new IdentityError
                    {
                        Code = "User does not exist",
                        Description = "Can not log in when user does not exist"
                    } }, 
                    Success = false,
                    Token = ""
                };
            var userHasValidPassword = await _userManager.CheckPasswordAsync(user, loginDTO.Password);
            if (!userHasValidPassword)
                return new AuthentificationResultDTO
                {
                    Errors = new List<IdentityError>{ new IdentityError
                {
                    Code = "Combination of login and password is wrong",
                    Description = "Combination of login and password is wrong"
                } },
                    Success = false,
                    Token = ""
                };
            var token = await GetTokenAsync(user);
            return new AuthentificationResultDTO { Token = token, Errors = null, Success = true };
        }
    }
}
