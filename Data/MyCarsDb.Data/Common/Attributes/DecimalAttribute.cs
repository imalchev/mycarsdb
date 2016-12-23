namespace MyCarsDb.Data.Common.Attributes
{
    using System;

    /// <summary>
    /// Uses SQL DECIMAL data type. By default uses DECIMAL(12, 2)
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public class DecimalAttribute : Attribute
    {
        public byte Precision { get; set; }
        public byte Scale { get; set; }

        public DecimalAttribute()
        {
            Precision = 12;
            Scale = 12;
        }

        public DecimalAttribute(byte precision, byte scale)
        {
            Precision = precision;
            Scale = scale;
        }
    }
}
