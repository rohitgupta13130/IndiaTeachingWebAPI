using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace India_Teaching.Models
{
    public class UserTypes
    {
        public int UserTypeId { get; set; }

        [DisplayName("User Type Name")]
        [Required(ErrorMessage = "User Type Name is required")]
        [StringLength(100, ErrorMessage = "User Type Name can't be longer than 100 characters")]
        public string UserTypeName { get; set; }
    }
}