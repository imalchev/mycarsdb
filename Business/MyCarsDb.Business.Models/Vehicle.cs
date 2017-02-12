namespace MyCarsDb.Business.Models
{
    using System;
    using System.Collections.Generic;

    using MyCarsDb.Business.Models.Enums;

    public class Vehicle
    {
        public int ModelId { get; set; }

        public int MakeId { get; set; }
        
        public string ExactModel { get; set; }
        
        public string RegNumber { get; set; }

        public DateTime ManufactureDate { get; set; }

        public VehicleType Type { get; set; }

        public int? Power { get; set; }

        public int? EngineCapacity { get; set; }

        public ICollection<FuelType> FuelTypes { get; set; }
    }
}
