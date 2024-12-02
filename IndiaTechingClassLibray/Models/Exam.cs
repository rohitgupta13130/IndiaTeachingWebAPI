using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace India_Teaching.Models
{
    public class Exam 
    {
        public List<Exam> ExamIndex { get; set; }
        public int Id { get; set; }
        [DisplayName("Exam Name")]
        [Required(ErrorMessage = "Please enter your exam name")]
        [StringLength(50, ErrorMessage = "Skill Name can't be longer than 50 characters")]
        [RegularExpression(@"^[a-zA-Z0-9]+(\s[a-zA-Z0-9]+)*$", ErrorMessage = "Special characters are not allowed, and spaces are only allowed between words")]
        public string ExamName { get; set; }
        public int ClassId { get; set; }
        public int SubjectId { get; set; }
       // public IEnumerable<SelectListItem> ClassList { get; set; }
      //  public IEnumerable<SelectListItem> SubjectList { get; set; }
        [Required(ErrorMessage = "Please select your class")]
        public string ClassName { get; set; }
        [Required(ErrorMessage = "Please select your subject")]
        public string SubjectName { get; set; }
    }
}