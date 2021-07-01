using AutoMapper;
using MessageService.API.Data.Dto;
using MessageService.API.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessageService.API.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Message, MessageDto>();
            CreateMap<MessageDto, Message>().ForMember(x => x.DateAdded, opt => { opt.Ignore(); });


            CreateMap<Reply, ReplyDto>();
            CreateMap<ReplyDto, Reply>().ForMember(x => x.DateAdded, opt => { opt.Ignore(); });

            CreateMap<Reply, ReplyListDto>();
            CreateMap<Message, MessageListDto>();

            CreateMap<LogDto, Log>().ReverseMap();

            CreateMap<Message, MessageBasicInfoDto>();

        }
    }
}
