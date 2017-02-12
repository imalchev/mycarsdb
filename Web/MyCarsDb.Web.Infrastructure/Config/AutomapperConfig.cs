namespace MyCarsDb.Web.Infrastructure.Config
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using AutoMapper;
    using AutoMapper.Configuration;

    using MyCarsDb.Common.Mappings;

    public static class AutoMapperConfig
    {
        public static MapperConfigurationExpression Configuration { get; private set; }

        public static void Execute(Assembly assembly)
        {
            Configuration = new MapperConfigurationExpression();
            var types = assembly.GetExportedTypes();
            LoadStandardMappings(types, Configuration);
            Mapper.Initialize(Configuration);
        }


        private static void LoadStandardMappings(IEnumerable<Type> types, IMapperConfigurationExpression mapperConfigurationExp)
        {
            var maps = (from t in types
                        where t.GetInterface(nameof(IObjectMapping)) == typeof(IObjectMapping)
                        select new
                        {
                            Method = t.GetMethod("CreateMap"),
                            TypeOfObj = t
                        }).ToList();

            foreach (var map in maps)
            {
                map.Method.Invoke(Activator.CreateInstance(map.TypeOfObj), new[] { mapperConfigurationExp });
            }
        }
    }
}