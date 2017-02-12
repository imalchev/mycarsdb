namespace MyCarsDb.Data.EntityFramework.Common
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.ModelConfiguration.Configuration;

    public static class Extensions
    {
        public static PrimitivePropertyConfiguration Index(this PrimitivePropertyConfiguration propertyConfig, string name, int order = 0, bool isUnique = false)
        {
            return propertyConfig
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute(name) { IsUnique = isUnique, Order = order }));
        }
    }
}
