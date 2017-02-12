namespace MyCarsDb.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using MyCarsDb.Data.Common;
    using MyCarsDb.Data.Models.Contracts;
    using MyCarsDb.Data.Models.Enums;    

    public class Vehicle : IEntity
    {
        private ICollection<UserToVehicle> _usersToVehicles;

        public Vehicle()
        {
            _usersToVehicles = new HashSet<UserToVehicle>();
        }

        public int Id { get; set; }

        public int ModelId { get; set; }

        public virtual VehicleModel Model { get; set; }

        [MaxLength(DataModelConstants.VEHICLE_REG_NUMBER_MAX_LENGTH)]
        public string RegNumber { get; set; }

        [MaxLength(DataModelConstants.VEHICLE_EXACT_MODEL_MAX_LENGTH)]
        public string ExactModel { get; set; }

        public DateTime ManufactureDate { get; set; }

        public VehicleType Type { get; set; }

        public int? Power { get; set; }

        public int? EngineCapacity { get; set; }

        public FuelType FuelTypes { get; set; }

        public virtual ICollection<UserToVehicle> UsersToVehicles
        {
            get { return _usersToVehicles; }
            set { _usersToVehicles = value; }
        }
    }
}
