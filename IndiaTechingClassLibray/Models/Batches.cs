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
        [Required(ErrorMessage = "Please Enter Your Batch Name")]
        [StringLength(100, ErrorMessage = "Batch Name can't be longer than 100 characters")]
        public string BatchName { get; set; }

        [DisplayName("Teacher Id")]
        [Required(ErrorMessage = "Please Enter Teacher Id")]
        public int TeacherId { get; set; }

        [DisplayName("Subject Id")]
        [Required(ErrorMessage = "Please Enter Subject Id")]
        public int SubjectId { get; set; }

        [DisplayName("Batch Start Time")]
        [Required(ErrorMessage = "Please Select Date And Time")]
        [DataType(DataType.DateTime, ErrorMessage = "Please enter a valid date and time")]
        public DateTime BatchStartTime { get; set; }

        [DisplayName("Batch End Time")]
        [Required(ErrorMessage = "Please Select Date And Time")]
        [DataType(DataType.DateTime, ErrorMessage = "Please enter a valid date and time")]
        public DateTime BatchEndTime { get; set; }

        //public Subject Subject { get; set; }

        //public Teacher Teacher { get; set; }

        //public List<SelectListItem> Teachers { get; set; }

        //public List<SelectListItem> Subjects { get; set; }

        public bool IsActive { get; set; }
    }
}