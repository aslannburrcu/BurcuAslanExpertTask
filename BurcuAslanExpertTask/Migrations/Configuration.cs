namespace BurcuAslanExpertTask.Migrations
{
    using BurcuAslanExpertTask.Database;
    using BurcuAslanExpertTask.Helpers;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<BurcuAslanExpertTask.Database.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(BurcuAslanExpertTask.Database.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.

            this.SeedUsers(context);
        }

        private void SeedUsers(ApplicationDbContext context)
        {
            var password = HashManager.HashPassword("Password123");
           if(!context.Users.Any(i => i.UserName =="user1@gmail.com"))
            {
                context.Users.Add(new Entities.User
                {
                    UserName = "user1@gmail.com",
                    Password = password //hash passwordu buraya yazdım
                });
            }

            if (!context.Users.Any(i => i.UserName == "user2@gmail.com"))
            {
                context.Users.Add(new Entities.User
                {
                    UserName = "user2@gmail.com",
                    Password = password //hash passwordu buraya yazdım
                });
            }
            context.SaveChanges();
        }
    }
}
