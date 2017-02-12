namespace MyCarsDb.Common.Mappings
{
    using AutoMapper;

    public interface IObjectMapping
    {
        void CreateMap(IMapperConfigurationExpression config);
    }
}
