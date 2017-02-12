namespace MyCarsDb.Data.EntityFramework.Common.AttributeConventions
{
    using System;
    using System.Data.Entity.ModelConfiguration.Configuration;
    using System.Data.Entity.ModelConfiguration.Conventions;

    using MyCarsDb.Data.Common.Attributes;

    public class TimeAttributeConvention : PrimitivePropertyAttributeConfigurationConvention<TimeAttribute>
    {
        public override void Apply(ConventionPrimitivePropertyConfiguration configuration, TimeAttribute attribute)
        {
            if (attribute.Precision < 0 || attribute.Precision > 7)
            {
                throw new InvalidOperationException("Precision must be between 0 and 7.");
            }

            configuration.HasPrecision(attribute.Precision);
        }
    }
}
