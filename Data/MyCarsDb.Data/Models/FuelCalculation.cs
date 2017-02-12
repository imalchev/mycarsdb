namespace MyCarsDb.Data.Models
{
    using System.ComponentModel.DataAnnotations.Schema;

    using MyCarsDb.Data.Common.Attributes;
    using MyCarsDb.Data.Models.Contracts;
    using MyCarsDb.Data.Models.Enums;    

    public class FuelCalculation : IEntity
    {
        public int Id { get; set; }

        [ForeignKey(nameof(FirstFueling))]
        public int FirstFuelingEventId { get; set; }
        [ForeignKey(nameof(FirstFuelingEventId))]
        public virtual Fueling FirstFueling { get; set; }

        [ForeignKey(nameof(LastFueling))]
        public int LastFuelingEventId { get; set; }

        [ForeignKey(nameof(LastFuelingEventId))]
        public virtual Fueling LastFueling { get; set; }

        public int CountFuelings { get; set; }

        public int Distance { get; set; }

        public FuelCalculationMethod CalculationMethod { get; set; }

        [Decimal(10, 3)]
        public decimal SpentFuelQuantity { get; set; }

        [Decimal(12, 2)]
        public decimal SpentMoneyAmount { get; set; }

        [Decimal(10, 5)]
        public decimal AverageFuelPerDistance { get; set; }

        [Decimal(10, 5)]
        public decimal AverageMoneyPerDistance { get; set; }
    }
}
