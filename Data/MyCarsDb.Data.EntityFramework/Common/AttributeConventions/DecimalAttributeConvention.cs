﻿namespace MyCarsDb.Data.EntityFramework.Common.AttributeConventions
{    
    using System;
    using System.Data.Entity.ModelConfiguration.Configuration;
    using System.Data.Entity.ModelConfiguration.Conventions;

    using MyCarsDb.Data.Common.Attributes;

    public class DecimalAttributeConvention : PrimitivePropertyAttributeConfigurationConvention<DecimalAttribute>
    {
        public override void Apply(ConventionPrimitivePropertyConfiguration configuration, DecimalAttribute attribute)
        {
            if (attribute.Precision < 1 || attribute.Precision > 38)
            {
                throw new InvalidOperationException("Precision must be between 1 and 38.");
            }

            if (attribute.Scale > attribute.Precision)
            {
                throw new InvalidOperationException("Scale must be between 0 and the Precision value.");
            }

            configuration.HasPrecision(attribute.Precision, attribute.Scale);
        }
    }
}
