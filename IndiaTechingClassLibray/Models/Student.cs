using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using India_Teaching.Enums;


namespace India_Teaching.Models
{
    public class Student 
    {
        public List<Student> StudentIndex { get; set; }
        public int Id { get; set; }

        [Required(ErrorMessage = "First Name is required")]
        [StringLength(50, ErrorMessage = "First Name can't be longer than 50 characters")]
        [RegularExpression(@"^[a-zA-Z0-9]+(\s[a-zA-Z0-9]+)*$", ErrorMessage = "Special characters are not allowed, and spaces are only allowed between words")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        [StringLength(50, ErrorMessage = "Last Name can't be longer than 50 characters")]
        [RegularExpression(@"^[a-zA-Z0-9]+(\s[a-zA-Z0-9]+)*$", ErrorMessage = "Special characters are not allowed, and spaces are only allowed between words")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Class is required")]
        public string Class { get; set; }


        [DisplayName("Country")]
        public EnumCountry Country { get; set; } = EnumCountry.Null;

        [StringLength(100, ErrorMessage = "School can't be longer than 100 characters")]
        [RegularExpression(@"^[a-zA-Z0-9]+(\s[a-zA-Z0-9]+)*$", ErrorMessage = "Special characters are not allowed, and spaces are only allowed between words")]
        public string School { get; set; }

        [StringLength(50, ErrorMessage = "Father's Name can't be longer than 50 characters")]
        [RegularExpression(@"^[a-zA-Z0-9]+(\s[a-zA-Z0-9]+)*$", ErrorMessage = "Special characters are not allowed, and spaces are only allowed between words")]
        public string FatherName { get; set; }

        [Required(ErrorMessage = "Mother's Name is required")]
        [StringLength(50, ErrorMessage = "Mother's Name can't be longer than 50 characters")]
        [RegularExpression(@"^[a-zA-Z0-9]+(\s[a-zA-Z0-9]+)*$", ErrorMessage = "Special characters are not allowed, and spaces are only allowed between words")]
        public string MotherName { get; set; }

        [Required(ErrorMessage = "Enrollment Date is required")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime Enrollmentdate { get; set; } = DateTime.Now;

       // public List<SelectListItem> Classes { get; set; }

        public string ClassName { get; set; }

       // public List<SelectListItem> Batches { get; set; }

        public List<int> SelectedBatchIds { get; set; }

        public bool IsActive { get; set; }

        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string Email { get; set; }

    }
}