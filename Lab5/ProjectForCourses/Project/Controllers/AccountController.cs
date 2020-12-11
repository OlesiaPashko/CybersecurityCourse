using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project.Models;
using AutoMapper;
using Project.Helpers;
using BLL.Services;
using System.IdentityModel.Tokens.Jwt;
using System.IdentityModel.Tokens;
using Project.Options;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Project.Contracts.V1.Requests;
using Project.Contracts.V1.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using BLL.DTOs;

namespace Project.Controllers
{
    //[Route("api/[controller]")]
    public class AccountsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IAccountService _accountService;
        public AccountsController(IMapper mapper, IAccountService accountService)
        {
            _mapper = mapper;
            _accountService = accountService;
        }

        // POST api/accounts/register
        [HttpPost]
        [Route("api/account/register")]
        public async Task<IActionResult> Post([FromBody]UserModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userDTO = _mapper.Map<UserDTO>(model);
            AuthentificationResultDTO authentificationResultDTO = await _accountService.RegisterAsync(userDTO);
            var authentificationResult = _mapper.Map<AuthentificationResult>(authentificationResultDTO);
            if (!authentificationResult.Success)
            {
                return BadRequest(authentificationResult);
            }
            return new OkObjectResult(authentificationResult);
        }

        // POST api/accounts/login
        [HttpPost]
        [Route("api/account/login")]
        public async Task<IActionResult> Post([FromBody]LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var loginDTO = _mapper.Map<LoginDTO>(model);
            AuthentificationResultDTO authentificationResultDTO = await _accountService.LoginAsync(loginDTO);
            var authentificationResult = _mapper.Map<AuthentificationResult>(authentificationResultDTO);
            if (!authentificationResult.Success)
            {
                return BadRequest(authentificationResult);
            }
            return new OkObjectResult(authentificationResult);
        }
    }
}
