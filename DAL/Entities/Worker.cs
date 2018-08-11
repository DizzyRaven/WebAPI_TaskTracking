using System.Collections;
using System.Collections.Generic;

namespace DAL.Entities
{
    public class Worker : User
    {
        public string Specification { get; set; }
        public ICollection<string> SkillStack { get; set; }
        public ICollection<Task> MyProperty { get; set; }
    }
}