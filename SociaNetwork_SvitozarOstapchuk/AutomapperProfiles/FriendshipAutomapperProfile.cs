using AutoMapper;
using BLL.DTOs;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.AutomapperProfiles
{
    public class FriendshipAutomapperProfile : Profile
    {
        public FriendshipAutomapperProfile()
        {
            CreateMap<FriendshipDTO, Friendship>();
            CreateMap<Friendship, FriendshipDTO>();
        }
    }
}