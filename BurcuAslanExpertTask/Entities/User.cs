using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BurcuAslanExpertTask.Entities
{
    public class User : EntityBase<int>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}