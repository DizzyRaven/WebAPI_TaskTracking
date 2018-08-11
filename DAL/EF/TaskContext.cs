using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Entity;
using DAL.Entities;

namespace DAL.EF
{
   public class TaskContext : DbContext
    {
        public DbSet<Task> Tasks { get; set; }
        public DbSet<SubTask> SubTasks { get; set; }
        public DbSet<TaskLabel> TaskLabels { get; set; }

        static TaskContext()
        {
            Database.SetInitializer<TaskContext>(new TaskTrackerDbDbInitializer());
        }

        public TaskContext(string connectionString)
            : base(connectionString)
        {
        }

       
        public class TaskTrackerDbDbInitializer : DropCreateDatabaseIfModelChanges<TaskContext>
        // Or use  DropCreateDatabaseAlways<TaskContext> to drop database every time.
        {
            protected override void Seed(TaskContext db)
            {
                db.TaskLabels.Add(new TaskLabel { Name = "Important" });
                db.TaskLabels.Add(new TaskLabel { Name = "Frozen" });

                //db.Tasks.Add(new Task { Name = "Test task 112", Description = "Just testing this task", CreationDate = DateTime.Now, DeadLine = DateTime.Now.AddDays(3), StartDate = DateTime.Now });
                //db.Tasks.Add(new Task { Name = "Test task 2", Description = "Just testing this task2", CreationDate = DateTime.Now, DeadLine = null, StartDate = DateTime.Now });
                //db.Tasks.Add(new Task { Name = "Test task 3", Description = "Just testing this task3", CreationDate = DateTime.Now, DeadLine = DateTime.Now.AddDays(5), StartDate = DateTime.Now });



                db.SaveChanges();
            }
        }
    }
}

