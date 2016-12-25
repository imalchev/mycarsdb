namespace MyCarsDb.Data.Models
{    
    using System;
    using System.ComponentModel.DataAnnotations;

    using MyCarsDb.Data.Models.Enums;

    public class Vehicle
    {
        public int Id { get; set; }

        public int ModelId { get; set; }

        public virtual VehicleModel Model { get; set; }

        [MaxLength(15)]
        public string RegNumber { get; set; }

        [MaxLength(50)]
        public string ExactModel { get; set; }

        public DateTime ManufactureDate { get; set; }

        public VehicleType Type { get; set; }

        public int? Power { get; set; }

        public int? EngineCapacity { get; set; }

        public FuelType FuelTypes { get; set; }
    }
}
