namespace MyCarsDb.Data.Common.Attributes
{
    using System;
    using System.Data.Entity.ModelConfiguration.Configuration;
    using System.Data.Entity.ModelConfiguration.Conventions;

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
