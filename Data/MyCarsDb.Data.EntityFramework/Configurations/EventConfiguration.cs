namespace MyCarsDb.Data.EntityFramework.Configurations
{    
    using System.Data.Entity.ModelConfiguration;

    using MyCarsDb.Data.EntityFramework.Common;
    using MyCarsDb.Data.Models;    

    public class EventConfiguration : EntityTypeConfiguration<Event>
    {
        private const string IX_VEHICLE_DATE_ODOMETER_ORDER = "IX_VehicleId_Date_Odometer_Order";
        private const string IX_DATE_VEHICLE = "IX_Date_VehicleId";

        public EventConfiguration()
        {
            this.Property(x => x.VehicleId)
                .Index(IX_VEHICLE_DATE_ODOMETER_ORDER, order: 1)
                .Index(IX_DATE_VEHICLE, order: 2);

            this.Property(x => x.Date)
                .Index(IX_VEHICLE_DATE_ODOMETER_ORDER, order: 2)
                .Index(IX_DATE_VEHICLE, order: 1);

            this.Property(x => x.Odometer)
                .Index(IX_VEHICLE_DATE_ODOMETER_ORDER, order: 3);

            this.Property(x => x.OrderIndex)
                .Index(IX_VEHICLE_DATE_ODOMETER_ORDER, order: 4);
        }
    }
}
