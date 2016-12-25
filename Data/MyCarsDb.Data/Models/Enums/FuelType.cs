namespace MyCarsDb.Data.Models.Enums
{
    using System;

    [Flags]
    public enum FuelType : short
    {
        Gasoline = 1,
        Diesel = 2,
        LPG = 4, // Liquefied petroleum gas
        CNG = 8,  // Compressed natural gas
        Electricity = 16
    }
}
