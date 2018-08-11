using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Performer : User
    {
        public Performer()
        {
            Tasks = new List<Task>();
        }
        public IEnumerable<Task> Tasks { get; set; }
    }
}
