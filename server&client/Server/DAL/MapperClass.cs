using AutoMapper;
using Entities;
using Entities.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class MapperClass
    {
        public static void OnInit()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<User, UserDto>();
                cfg.CreateMap<UserDto, User>();
               /* cfg.CreateMap<UserDto, User>().ForMember(u => u.mail, e => e.MapFrom(en => en.mail))
                                              .ForMember(u => u.name, e => e.MapFrom(en => en.name))
                                              .ForMember(u => u.password, e => e.MapFrom(en => en.password));*/
                cfg.CreateMap<Request, RequestDto>();
                cfg.CreateMap<RequestDto, Request>();
            });
        }
    }
}
