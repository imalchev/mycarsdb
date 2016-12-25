namespace MyCarsDb.Data.Models
{    
    using System.ComponentModel.DataAnnotations;

    using MyCarsDb.Data.Models.Enums;

    public class VehicleModel
    {
        public int Id { get; set; }

        public int MakeId { get; set; }
        public VehicleMake Make { get; set; }

        public VehicleType VehicleType { get; set; }

        [MaxLength(50)]
        public string ModelName { get; set; }
    }
}
