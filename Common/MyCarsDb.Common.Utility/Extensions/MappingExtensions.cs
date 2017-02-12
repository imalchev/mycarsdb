namespace MyCarsDb.Common.Utility.Extensions
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    public static class MappingExtensions
    {
        public static TDestination MapTo<TSource, TDestination>(this TSource source)
        {
            return Mapper.Map<TDestination>(source);
        }

        public static IQueryable<TDestination> ProjectTo<TDestination>(this IQueryable source, params Expression<Func<TDestination, object>>[] membersToExpand)
        {
            return source.ProjectTo(Mapper.Configuration, membersToExpand);
        }
    }
}
