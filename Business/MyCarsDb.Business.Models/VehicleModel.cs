namespace MyCarsDb.Business.Models
{
    using MyCarsDb.Business.Models.Enums;

    public class VehicleModel
    {
        public int Id { get; set; }

        public VehicleType VehicleType { get; set; }
        
        public string ModelName { get; set; }
    }
}
