using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace India_Teaching.Request
{
    public class LoginRequest
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public int UserType { get; set; }
    }
}