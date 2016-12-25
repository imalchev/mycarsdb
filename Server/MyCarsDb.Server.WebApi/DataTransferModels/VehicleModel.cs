namespace MyCarsDb.Server.WebApi.DataTransferModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Collections.Generic;

    using MyCarsDb.Data.Models.Enums;

    public class VehicleModel
    {
        public int ModelId { get; set; }

        [MaxLength(50)]
        public string ExactModel { get; set; }

        public DateTime ManufactureDate { get; set; }

        public VehicleType Type { get; set; }

        public int? Power { get; set; }

        public int? EngineCapacity { get; set; }

        public IEnumerable<FuelType> FuelTypes { get; set; }
    }
}