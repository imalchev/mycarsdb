namespace MyCarsDb.Data.Models
{
    using MyCarsDb.Data.Models.Enums;
    using System.ComponentModel.DataAnnotations;

    public class VehicleModel
    {
        public int Id { get; set; }

        public VehicleType VehicleType { get; set; }

        [MaxLength(50)]
        public string ModelName { get; set; }
    }
}
