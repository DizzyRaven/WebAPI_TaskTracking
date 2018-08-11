using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Entities
{
    // TODO refact class
    public abstract class User
    {
        public int Id { get; set; }
        public string NickName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        // TODO Email validation
        public string Email { get; set; }
       // public string TeamColor { get; set; }
       // public Board Board {get;set;}
    }
}
