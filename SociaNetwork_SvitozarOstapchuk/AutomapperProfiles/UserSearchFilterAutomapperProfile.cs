using AutoMapper;
using BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebApi.AutomapperProfiles
{
    public class UserSearchFilterAutomapperProfile : Profile
    {
        public UserSearchFilterAutomapperProfile()
        {
            CreateMap<UserSearchFilterDTO, UserSearchFilterModel>();
            CreateMap<UserSearchFilterModel, UserSearchFilterDTO>();
        }
    }
}
