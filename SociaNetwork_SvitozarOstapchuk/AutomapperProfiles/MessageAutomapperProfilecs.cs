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
    public class MessageAutomapperProfile : Profile
    {
        public MessageAutomapperProfile()
        {
            CreateMap<MessageDTO, Message>();
            CreateMap<Message, MessageDTO>();
            CreateMap<MessageDTO, MessageModel>();
            CreateMap<MessageModel, MessageDTO>()
                .ForMember(x => x.MessageText, opt => opt.MapFrom(m => m.MessageText))
                .ForMember(x => x.TimeSent, opt => opt.MapFrom(o => DateTime.Now))
                .ForMember(x => x.SenderUsername, opt => opt.MapFrom(m => m.User1))
                .ForMember(x => x.ReceiverUsername, opt => opt.MapFrom(m => m.User2));
        }
    }
}

