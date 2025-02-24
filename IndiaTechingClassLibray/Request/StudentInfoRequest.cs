using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace India_Teaching.Request
{
    public class StudentInfoRequest
    {
        public int Id { get; set; }
        public string ContactNo { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string ProfileImagePath { get; set; }
    }
}