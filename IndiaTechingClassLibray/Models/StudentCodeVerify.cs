using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace India_Teaching.Models
{
    public class StudentCodeVerify
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Current Status is required")]
        public int CurrentStatus { get; set; }

        [Required(ErrorMessage = "Code is required")]
        [StringLength(20, ErrorMessage = "Code can't be longer than 20 characters")]
        [RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "Special characters are not allowed in the Code")]
        public string Code { get; set; }
    }
}