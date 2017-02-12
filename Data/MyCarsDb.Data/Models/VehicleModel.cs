namespace MyCarsDb.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using MyCarsDb.Data.Common;
    using MyCarsDb.Data.Models.Contracts;
    using MyCarsDb.Data.Models.Enums;    

    public class VehicleModel : IEntity
    {
        public int Id { get; set; }

        public int MakeId { get; set; }
        public virtual VehicleMake Make { get; set; }

        public VehicleType VehicleType { get; set; }

        [MaxLength(DataModelConstants.VEHICLE_MODEL_MODEL_NAME_MAX_LENGTH)]
        public string ModelName { get; set; }
    }
}
