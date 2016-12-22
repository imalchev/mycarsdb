namespace MyCarsDb.Data.Models
{
    using System;

    public class Event
    {
        public DateTime Date { get; set; }

        public int? Odometer { get; set; }

        public virtual Vehicle Vehicle { get; set; }

        public int OrderIndex { get; set; }
    }
}