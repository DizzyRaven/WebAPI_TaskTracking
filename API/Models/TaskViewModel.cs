using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class TaskViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? TaskLabelId { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? DeadLine { get; set; }
    }
}