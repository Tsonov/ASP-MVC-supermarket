using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Supermarket.Core.Models;
using Supermarket.Main.Areas.Management.Models;

namespace Supermarket.Main
{
    public class MapperConfig
    {
        public static void RegisterMaps()
        {
            Mapper.CreateMap<UserProfile, UserInfoViewModel>()
                .ForMember(destination => destination.Id, expression => expression.MapFrom(source => source.UserId));
            //Mapper.Creat
        }
    }
}