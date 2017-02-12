namespace MyCarsDb.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using MyCarsDb.Data.Common;
    using MyCarsDb.Data.Models.Contracts;    

    public class VehicleMake : IEntity
    {
        public int Id { get; set; }

        [MaxLength(DataModelConstants.VEHICLE_MAKE_NAME_MAX_LENGTH)]
        public string Name { get; set; }
    }
}
