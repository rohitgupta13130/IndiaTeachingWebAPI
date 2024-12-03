using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace India_Teaching.Models
{
    public class Notification 
    {
        public List<Notification> NotificationIndex { get; set; }
        public int Id { get; set; }
        public string To { get; set; }

        [Display(Name = "Teacher")]
        [Required(ErrorMessage = "Teacher is required")]
        public int TeacherId { get; set; }

        [Required(ErrorMessage = "Notification Text is required")]
        [RegularExpression(@"^(?!.*((http|https):\/\/|www\.))[^<>]*$", ErrorMessage = "Notification Text should not contain links, URLs, or HTML tags")]
        public string NotificationText { get; set; }


        [Required(ErrorMessage = "Create Date Time is required")]
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
        public bool IsActive { get; set; }
       // public IEnumerable<SelectListItem> BatchList { get; set; }

        [Display(Name = "Batch")]
        [Required(ErrorMessage = "BatchName is required")]
        public int BatchId { get; set; }
        public Studentbatches Studentbatches { get; set; }
       // public IEnumerable<SelectListItem> TeacherList { get; set; }
        public string BatchName { get; set; }

        public string TeacherName { get; set; }
    }
}