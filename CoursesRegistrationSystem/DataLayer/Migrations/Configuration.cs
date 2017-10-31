namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;

            #if DEBUG
                this.AutomaticMigrationDataLossAllowed = true;
            #endif

            this.AutomaticMigrationDataLossAllowed = false;
        }

        protected override void Seed(DataLayer.ApplicationDbContext context)
        {
        }
    }
}