using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace India_Teaching.Models
{
    public class StudentInfo
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Contact Number is required")]
        [Phone(ErrorMessage = "Please enter a valid phone number")]
        public string ContactNo { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "User Name is required")]
        [StringLength(50, ErrorMessage = "User Name can't be longer than 50 characters")]
        public string UserName { get; set; }

        [StringLength(200, ErrorMessage = "Profile Image Path can't be longer than 200 characters")]
        [Url(ErrorMessage = "Please enter a valid URL for the profile image path")]
        public string ProfileImagePath { get; set; }
    }
}