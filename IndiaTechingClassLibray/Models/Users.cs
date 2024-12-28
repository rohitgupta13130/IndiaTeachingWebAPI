using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace India_Teaching.Models
{
    public class Users
    {
        public int UserId { get; set; }

        [DisplayName("User Name")]
        [Required(ErrorMessage = "User Name is required")]
        [StringLength(50, ErrorMessage = "User Name can't be longer than 50 characters")]
        public string UserName { get; set; }

        [DisplayName("User Password")]
        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Password must be between 6 and 100 characters")]
        public string UserPassword { get; set; }


        [DisplayName("User Type")]
        [Required(ErrorMessage = "User Type is required")]
        public int UserType { get; set; }

        public List<UserTypes> lst { get; set; }
    }
}