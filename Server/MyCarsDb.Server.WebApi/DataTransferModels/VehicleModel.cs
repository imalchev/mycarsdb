namespace MyCarsDb.Server.WebApi.DataTransferModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Collections.Generic;

    using MyCarsDb.Data.Models.Enums;
    using Newtonsoft.Json;

    public class VehicleModel
    {
        public int VehicleId { get; set; }

        public int ModelId { get; set; }
  
        public int MakeId { get; set; }

        [MaxLength(50)]
        public string ExactModel { get; set; }

        [MaxLength(15)]
        public string RegNumber { get; set; }

        public DateTime ManufactureDate { get; set; }

        public VehicleType Type { get; set; }

        public int? Power { get; set; }

        public int? EngineCapacity { get; set; }

        public IList<FuelType> FuelTypes { get; set; }
    }
}