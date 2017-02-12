namespace MyCarsDb.Data.EntityFramework.Configurations
{
    using System.ComponentModel.DataAnnotations.Schema;    
    using System.Data.Entity.ModelConfiguration;

    using MyCarsDb.Data.Models;

    public class FuelingConfiguration: EntityTypeConfiguration<Fueling>
    {
        public FuelingConfiguration()
        {
            ToTable("Fuelings");

            HasKey(flng => flng.EventId);

            Property(flng => flng.EventId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
    }
    }
}
