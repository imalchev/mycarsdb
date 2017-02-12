namespace MyCarsDb.Common.Mappings
{
    using System;

    using AutoMapper;

    using Microsoft.AspNet.Identity;

    using BM = MyCarsDb.Business.Models;
    using DM = MyCarsDb.Data.Models;

    public class IdentityMappings : IObjectMapping
    {
        public void CreateMap(IMapperConfigurationExpression config)
        {
            config.CreateMap<BM.RegisterUser, DM.User>()
                .ForMember(regUser => regUser.UserName, opt => opt.MapFrom(u => u.Email));

            config.CreateMap<IdentityResult, BM.IdentityResult>();

            config.CreateMap<BM.User, DM.User>()
                .ForMember(dbUser => dbUser.UserName, opt => opt.MapFrom(u => u.Email));

            config.CreateMap<DM.User, BM.User>()
                .ForMember(boUser => boUser.Email, opt => opt.MapFrom(u => u.UserName));
        }
    }
}
