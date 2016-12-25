namespace MyCarsDb.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Event
    {
        public int Id { get; set; }

        [Index("IX_VehicleId_Date_Odometer_Order", Order = 1)]
        [Index("IX_Date_Vehicle", Order = 2)]
        public int VehicleId { get; set; }
        public virtual Vehicle Vehicle { get; set; }

        [Index("IX_VehicleId_Date_Odometer_Order", Order = 2)]
        [Index("IX_Date_Vehicle", Order = 1)]
        public DateTime Date { get; set; }

        [Index("IX_VehicleId_Date_Odometer_Order", Order = 3)]
        public int? Odometer { get; set; }

        [Index("IX_VehicleId_Date_Odometer_Order", Order = 4)]
        public int OrderIndex { get; set; }

        public virtual Fueling Fueling { get; set; }
    }
}