using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web;


namespace India_Teaching.Models
{
    public class Batches 
    {
        public List<Batches> BatchesIndex { get; set; }

        public int Id { get; set; }

        [DisplayName("Batch Name")]
        [Required(ErrorMessage = "Please Enter Batch Name")]
        [StringLength(100, ErrorMessage = "Batch Name can't be longer than 100 characters")]
        [RegularExpression(@"^[a-zA-Z@#/_-]+$", ErrorMessage = "Only alphabets and the characters -_@#/ are allowed")]
        public string BatchName { get; set; }


        [DisplayName("Teacher")]
        [Required(ErrorMessage = "Please Select Teacher")]
        public int TeacherId { get; set; }

        [DisplayName("Subject")]
        [Required(ErrorMessage = "Please Select Subject")]
        public int SubjectId { get; set; }

        [DisplayName("Batch Start Time")]
        [Required(ErrorMessage = "Please Select Date And Time")]
        [DataType(DataType.DateTime, ErrorMessage = "Please enter a valid date and time")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-ddTHH:mm}", ApplyFormatInEditMode = true)]
        public DateTime BatchStartTime { get; set; }


        [DisplayName("Batch End Time")]
        [Required(ErrorMessage = "Please Select Date And Time")]
        [DataType(DataType.DateTime, ErrorMessage = "Please enter a valid date and time")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-ddTHH:mm}", ApplyFormatInEditMode = true)]
        public DateTime BatchEndTime { get; set; }

        public Subject Subject { get; set; }

        public Teacher Teacher { get; set; }

       // public List<SelectListItem> Teachers { get; set; }

       // public List<SelectListItem> Subjects { get; set; }

        public bool IsActive { get; set; }
    }
}