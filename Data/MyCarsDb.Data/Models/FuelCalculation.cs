using MyCarsDb.Data.Common.Attributes;

namespace MyCarsDb.Data.Models
{
    public class FuelCalculation
    {
        public int Id { get; set; }

        public int StartFuelingId { get; set; }
        public virtual Fueling StartFueling { get; set; }

        public int EndFuelingId { get; set; }
        public virtual Fueling EndFueling { get; set; }

        public int CountFuelings { get; set; }

        public int Distance { get; set; }

        [Decimal(10, 3)]
        public decimal SpentFuelQuantity { get; set; }

        [Decimal(12, 2)]
        public decimal SpentMoneyAmount { get; set; }

        [Decimal(10, 3)]
        public decimal AverageFuelPer100 { get; set; }

        [Decimal(12, 2)]
        public decimal AverageMoneyPer100 { get; set; }
    }
}
