namespace MyCarsDb.Data.Common.Attributes
{
    using System;

    /// <summary>
    /// Use SQL TIME data type. By default uses TIME(0)
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public class TimeAttribute : Attribute
    {
        public byte Precision { get; set; }

        /// <summary>
        /// By defaut uses TIME(0)
        /// </summary>
        public TimeAttribute()
        {
            Precision = 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="precision">TIME(X) X must be between 0 and 7</param>
        public TimeAttribute(byte precision)
        {
            Precision = precision;
        }
    }
}
