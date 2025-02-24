using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace India_Teaching.Request
{
    public class HomeWorkRequest
    {
        public int Id { get; set; }
        public int ClassId { get; set; }
        public int SubjectId { get; set; }
        public int TeacherId { get; set; }
        public string Homework { get; set; }

    }
}