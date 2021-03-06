﻿namespace MyCarsDb.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using MyCarsDb.Data.Common;
    using MyCarsDb.Data.Common.Attributes;
    using MyCarsDb.Data.Models.Contracts;
    using MyCarsDb.Data.Models.Enums;    

    public class Fueling : IEntity
    {
        [Key]
        [ForeignKey(nameof(Event))]
        public int EventId { get; set; }
        [Required]
        public virtual Event Event { get; set; }

        public bool SeriesBegining { get; set; }

        public FuelType FuelType { get; set; }

        public bool IsFull { get; set; }

        public bool IsEmpty { get; set; }

        [Decimal(10, 3)]
        public decimal Quantity { get; set; }

        [Decimal(12, 2)]
        public decimal? Amount { get; set; }

        [MaxLength(DataModelConstants.FUELING_NOTE_MAX_LENGTH)]
        public string Note { get; set; }
    }
}
