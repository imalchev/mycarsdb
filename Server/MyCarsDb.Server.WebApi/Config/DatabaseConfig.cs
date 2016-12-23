namespace MyCarsDb.Server.WebApi.Config
{    
    using System.Data.Entity;

    using MyCarsDb.Data;
    using MyCarsDb.Data.Migrations;

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