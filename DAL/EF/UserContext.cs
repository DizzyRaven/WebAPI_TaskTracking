using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Entity;
using DAL.Entities;

namespace DAL.EF
{
    public class UserContext : DbContext
    {
        public DbSet<Performer> Performers { get; set; }
        public DbSet<Manager> Managers { get; set; }

        static UserContext()
        {
            Database.SetInitializer(new UsersDbInintializer());
        }
         public UserContext(string connectionString)
            : base(connectionString)
        {

        }

    }
    public class UsersDbInintializer : DropCreateDatabaseIfModelChanges<UserContext>
    {
        protected override void Seed(UserContext db)
        {
            db.Performers.Add(new Performer { NickName = "fstPerformer", FirstName = "Ivan", LastName = "Pupkin", Email = "ivanpupksdf@gmail.com" });
            db.Managers.Add(new Manager { NickName = "Manager", FirstName = "Johnny", LastName = "Smith", Email = "john221f@gmail.com" });
            db.SaveChanges();
        }
    }
}
