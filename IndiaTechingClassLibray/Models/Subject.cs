using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace India_Teaching.Models
{
    public class Subject 
    {
        public List<Subject> SubjectIndex { get; set; }

        public int ID { get; set; }

        [DisplayName("Subject Name")]
        [Required(ErrorMessage = "Subject Name is required")]
        [StringLength(100, ErrorMessage = "Subject Name can't be longer than 100 characters")]
        public string SubjectName { get; set; }

        public bool IsActive { get; set; }

    }
}