using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace India_Teaching.Request
{
    public class StudentCodeVerifyRequest
    {

        public int Id { get; set; }
        public int CurrentStatus { get; set; }
        public string Code { get; set; }
    }
}