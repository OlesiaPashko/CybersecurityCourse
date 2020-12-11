using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Contracts.V1.Requests;
using BLL.Services;
using BLL.DTOs;

namespace Project.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public UsersController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        private UserModel UserDTOToUserModel(UserDTO userDTO)
        {
            return new UserModel
            {
                FirstName = userDTO.FirstName,
                LastName = userDTO.LastName,
                UserName = userDTO.UserName,
                Email = userDTO.Email,
                Password = userDTO.Password
            };
        }

        private UserDTO UserModelToUserDTO(UserModel user)
        {
            return new UserDTO
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Email = user.Email,
                Password = user.Password
            };
        }
        // GET: api/Users
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _userService.GetUsersAsync());
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var post = await _userService.GetUserByIdAsync(id);

            if (post == null)
                return NotFound();
            return Ok(post);
        }

        // POST: api/Users
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserModel userModel)
        {
            var user = UserModelToUserDTO(userModel);
            await _userService.CreateUserAsync(user);
            return Ok("User was created");
        }

        // PUT: api/Users/5
        [HttpPut("{userId}")]
        public async Task<IActionResult> Put(Guid userId, [FromBody] UserModel updatedUser)
        {
            var user = await _userService.GetUserByIdAsync(userId);
            var isUpdated = await _userService.UpdateUserAsync(user);
            if (isUpdated)
                return Ok(user);
            return NotFound();
        }

        // DELETE: api/Users/4
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid userId)
        {
            var isDeleted = await _userService.DeleteUserAsync(userId);
            if (!isDeleted)
                return NotFound();
            return NoContent();
        }
    }
}