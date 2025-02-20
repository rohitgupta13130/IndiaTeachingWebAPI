using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace India_Teaching.Models
{
    public class UserAccess
    {
        public int Id { get; set; }
        public int ControllerActionId { get; set; }
        public string ControllerAction { get; set; }
        public bool Add { get; set; }
        public bool Edit { get; set; }
        public bool View { get; set; }
        public bool Search { get; set; }
        public string MenuName { get; set; }
        public bool SpecialAccess { get; set; }
    }
}