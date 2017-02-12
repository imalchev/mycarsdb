namespace MyCarsDb.Web.Infrastructure.Config
{
    using System.Data.Entity;
        
    using MyCarsDb.Data.EntityFramework;
    using MyCarsDb.Data.EntityFramework.Migrations;

    public class DatabaseConfig
    {
        public static void Initialize()
        {
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<MyCarsDbContext, Configuration>());
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<MyCarsDbContext>());

            using (var db = MyCarsDbContext.Create())
            {
                db.Database.Initialize(force: true);
            }
        }
    }
}