using System;
using System.Collections.Generic;


namespace DAL.Entities
{
   public class Task
    {
        public Task()
        {
            SubTasks = new List<SubTask>();
        }

        public int Id { get; set; }
        public int? LabelId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? DeadLine { get; set; }
        public DateTime CreationDate { get; set; }

        //public Worker Worker { get; set; }
        public virtual ICollection<SubTask> SubTasks { get; set; }
        public virtual TaskLabel Label { get; set; }

      
    }
}
