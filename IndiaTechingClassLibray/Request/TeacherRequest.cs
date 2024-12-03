using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace India_Teaching.Request
{
    public class TeacherRequest
    {
        public int TeacherID { get; set; }
        public string Fullname { get; set; }
        public DateTime DateofBirth { get; set; }
        public string MobileNumber { get; set; }
        public string Address { get; set; }
        public string Qualification { get; set; }
        public string Married { get; set; }
        public string ProfileLink { get; set; }
        public string VideoLink { get; set; }
        public int SharePercentage { get; set; }

    }
}