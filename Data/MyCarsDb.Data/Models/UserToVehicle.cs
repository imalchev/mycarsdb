namespace MyCarsDb.Data.Models
{
    using MyCarsDb.Data.Models.Enums;

    public class UserToVehicle
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }

        public int VehicleId { get; set; }
        public virtual Vehicle Vehicle { get; set; }

        public UserToVehicleAccessType AccessType { get; set; }
    }
}
