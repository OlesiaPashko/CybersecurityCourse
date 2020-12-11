using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BLL.DTOs;
using Project.Contracts.V1.Requests;
using Project.Contracts.V1.Responses;

namespace Project.Models.Mappings
{
    public class ViewModelToDTOMappingProfile:Profile
    {
        public ViewModelToDTOMappingProfile()
        {
            CreateMap<UserModel, UserDTO>().ReverseMap();
            CreateMap<AuthentificationResult, AuthentificationResultDTO>().ReverseMap();
            CreateMap<LoginModel, LoginDTO>().ReverseMap();
        }
    }
}
