using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Contracts.V1.Requests;
using Project.Extentions;

namespace Project.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UserProfileController : ControllerBase
    {
        private readonly IUserProfileService _userProfileService;

        public UserProfileController(IUserProfileService userProfileService)
        {
            _userProfileService = userProfileService;
        }
        //Get : "api/UserProfile"
        [HttpGet]
        [Authorize]
        public async Task<UserModel> GetUserProfile()
        {
            var UserId = HttpContext.GetUserId();
            var user = await _userProfileService.GetUserProfile(UserId);
            return new UserModel
            {
                UserName = user.UserName,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName
            };
        }
    }
}