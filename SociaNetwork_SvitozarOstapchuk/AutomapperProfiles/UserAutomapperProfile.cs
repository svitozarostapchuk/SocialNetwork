using AutoMapper;
using BLL.DTOs;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebApi.AutomapperProfiles
{
    public class UserAutomapperProfile : Profile
    {
        public UserAutomapperProfile()
        {
            CreateMap<UserDTO, UserModel>();
            CreateMap<UserModel, UserDTO>();
            CreateMap<User, UserDTO>()
               .ForMember(userDto => userDto.City, configExpression => configExpression.MapFrom(user => user.UserProfile.City))
               .ForMember(userDto => userDto.Age, configExpression => configExpression.MapFrom(user => user.UserProfile.Age))
               .ForMember(userDto => userDto.School, configExpression => configExpression.MapFrom(user => user.UserProfile.School))
               .ForMember(userDto => userDto.University, configExpression => configExpression.MapFrom(user => user.UserProfile.University))
               .ForMember(userDto => userDto.AboutUser, configExpression => configExpression.MapFrom(user => user.UserProfile.AboutUser))
               .ReverseMap();
        }
    }
}
