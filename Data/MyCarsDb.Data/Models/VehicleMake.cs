namespace MyCarsDb.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class VehicleMake
    {
        public int Id { get; set; }

        [MaxLength]
        public string Name { get; set; }
    }
}
