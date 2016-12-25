namespace MyCarsDb.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using MyCarsDb.Data.Models.Base;
    using MyCarsDb.Data.Common.Attributes;
    using MyCarsDb.Data.Models.Enums;

    public class Fueling
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

        [MaxLength(200)]
        public string Note { get; set; }
    }
}
