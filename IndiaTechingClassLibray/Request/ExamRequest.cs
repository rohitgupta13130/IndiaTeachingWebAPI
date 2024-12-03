using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace India_Teaching.Request
{
    public class ExamRequest
    {
        public int Id { get; set; }
        public string ExamName { get; set; }
        public int ClassId { get; set; }
        public int SubjectId { get; set; }
    }
}