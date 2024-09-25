using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace India_Teaching.Models
{
    public class Student 
    {
        public List<Student> StudentIndex { get; set; }
        public int Id { get; set; }

        [Required(ErrorMessage = "First Name is required")]
        [StringLength(50, ErrorMessage = "First Name can't be longer than 50 characters")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        [StringLength(50, ErrorMessage = "Last Name can't be longer than 50 characters")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Class is required")]
        [StringLength(20, ErrorMessage = "Class can't be longer than 20 characters")]
        public string Class { get; set; }

        [Required(ErrorMessage = "Country is required")]
        [StringLength(50, ErrorMessage = "Country can't be longer than 50 characters")]
        public string Country { get; set; }

        [Required(ErrorMessage = "School is required")]
        [StringLength(100, ErrorMessage = "School can't be longer than 100 characters")]
        public string School { get; set; }

        [Required(ErrorMessage = "Father's Name is required")]
        [StringLength(50, ErrorMessage = "Father's Name can't be longer than 50 characters")]
        public string FatherName { get; set; }

        [Required(ErrorMessage = "Mother's Name is required")]
        [StringLength(50, ErrorMessage = "Mother's Name can't be longer than 50 characters")]
        public string MotherName { get; set; }

        [Required(ErrorMessage = "Enrollment Date is required")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime Enrollmentdate { get; set; } = DateTime.Now;

        //public List<SelectListItem> Classes { get; set; }

        public string ClassName { get; set; }

        //public List<SelectListItem> Batches { get; set; }

        public List<int> SelectedBatchIds { get; set; }

        public bool IsActive { get; set; }

    }
}