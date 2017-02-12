namespace MyCarsDb.Data.Models
{    
    using System;

    using MyCarsDb.Data.Models.Contracts;

    public class Event : IEntity
    {
        public int Id { get; set; }
        
        public int VehicleId { get; set; }
        public virtual Vehicle Vehicle { get; set; }

        public DateTime Date { get; set; }
        
        public int? Odometer { get; set; }

        public int OrderIndex { get; set; }

        public virtual Fueling Fueling { get; set; }
    }
}