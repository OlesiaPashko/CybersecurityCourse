using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BLL.DTOs;
using DLL.Entities;

namespace BLL.Mappings
{
    public class EntityToDTOMappingProfile:Profile
    {
        public EntityToDTOMappingProfile()
        {
            CreateMap<UserDTO, User>().ForMember(entity => entity.Email, map => map.MapFrom(dto => dto.Email))
            .ForMember(entity => entity.FirstName, map => map.MapFrom(dto => dto.FirstName))
            .ForMember(entity => entity.LastName, map => map.MapFrom(dto => dto.LastName))
            .ForMember(entity => entity.UserName, map => map.MapFrom(dto => dto.UserName))
            .ForMember(entity => entity.AccessFailedCount, opt => opt.Ignore())
             .ForMember(entity => entity.ConcurrencyStamp, opt => opt.Ignore())
             .ForMember(entity => entity.EmailConfirmed, opt => opt.Ignore())
             .ForMember(entity => entity.LockoutEnabled, opt => opt.Ignore())
             .ForMember(entity => entity.LockoutEnd, opt => opt.Ignore())
             .ForMember(entity => entity.NormalizedEmail, opt => opt.Ignore())
             .ForMember(entity => entity.NormalizedUserName, opt => opt.Ignore())
             .ForMember(entity => entity.PhoneNumber, opt => opt.Ignore())
             .ForMember(entity => entity.PhoneNumberConfirmed, opt => opt.Ignore())
             .ForMember(entity => entity.SecurityStamp, opt => opt.Ignore())
             .ForMember(entity => entity.TwoFactorEnabled, opt => opt.Ignore())
              .ReverseMap();
        }
    }
}
