using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace India_Teaching.Models
{
    public class Notification 
    {
        public List<Notification> NotificationIndex { get; set; }
        public int Id { get; set; }
        public string To { get; set; }
        public int TeacherId { get; set; }
        public string NotificationText { get; set; }
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
        public bool IsActive { get; set; }
        //public IEnumerable<SelectListItem> BatchList { get; set; }
        public int BatchId { get; set; }
        public Studentbatches Studentbatches { get; set; }
        //public IEnumerable<SelectListItem> TeacherList { get; set; }
        public string BatchName { get; set; }

        public string TeacherName { get; set; }
    }
}