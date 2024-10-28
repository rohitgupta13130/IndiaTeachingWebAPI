using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace India_Teaching.Request
{
    public class NotificationRequest
    {
        public int Id { get; set; }
        public int TeacherId { get; set; }
        public string NotificationText { get; set; }
        public int BatchId { get; set; }
        public DateTime CreatedDateTime { get; set; }
    }
}