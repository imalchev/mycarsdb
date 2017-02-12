namespace MyCarsDb.Business.Models.Enums
{
    using System;

    [Flags]
    public enum FuelType : int
    {
        Gasoline = 1,
        Diesel = 2,
        LPG = 4, // Liquefied petroleum gas
        CNG = 8,  // Compressed natural gas
        Electricity = 16,
        Hydrogen = 32 // H         
    }
}
