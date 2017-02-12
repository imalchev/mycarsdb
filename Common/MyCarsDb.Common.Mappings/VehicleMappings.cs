namespace MyCarsDb.Common.Mappings
{
    using System;

    using AutoMapper;

    using BM = MyCarsDb.Business.Models;
    using DM = MyCarsDb.Data.Models;

    public class VehicleMappings : IObjectMapping
    {
        public void CreateMap(IMapperConfigurationExpression config)
        {
            config.CreateMap<DM.Vehicle, BM.Vehicle>().ReverseMap();
        }
    }
}
