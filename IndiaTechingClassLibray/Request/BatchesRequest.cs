using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace India_Teaching.Request
{
    public class BatchesRequest
    {

        public int Id { get; set; }
        public string BatchName { get; set; }
        public int TeacherId { get; set; }

        public int SubjectId { get; set; }
        public DateTime BatchStartTime { get; set; }
        public DateTime BatchEndTime { get; set; }
        public bool IsActive { get; set; }
    }
}