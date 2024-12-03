using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace India_Teaching.Request
{
    public class SubjectRequest
    {
        public int ID { get; set; }
        public string SubjectName { get; set; }
        public bool IsActive { get; set; }
    }
}