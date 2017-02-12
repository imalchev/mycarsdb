namespace MyCarsDb.Data.Models.Contracts
{
    using System;

    public interface IDeletableEntity : IEntity
    {
        DateTime? DeletedOn { get; set; }

        int? DeletedByUserId { get; set; }
    }
}
