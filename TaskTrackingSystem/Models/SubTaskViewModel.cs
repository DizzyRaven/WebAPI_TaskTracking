using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskTrackingSystem.Models
{
    public class SubTaskViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public int TaskId { get; set; }
        //public Task Task { get; set; }
    }
}