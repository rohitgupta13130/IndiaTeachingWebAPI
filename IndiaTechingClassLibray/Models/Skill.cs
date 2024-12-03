using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using India_Teaching.Enums;

namespace IndiaTechingClassLibray.Models
{
    public class Skill
    {
        public List<Skill> SkillIndex { get; set; }

        [DisplayName("Skill Id")]
        [Required(ErrorMessage = "Please Enter Correct Id")]
        public int SkillId { get; set; }

        [DisplayName("Skill Name")]
        [Required(ErrorMessage = "Please enter your skill name")]
        [StringLength(50, ErrorMessage = "Skill Name can't be longer than 50 characters")]
        [RegularExpression(@"^[a-zA-Z0-9]+(\s[a-zA-Z0-9]+)*$", ErrorMessage = "Special characters are not allowed, and spaces are only allowed between words")]
        public string SkillName { get; set; }


        [DisplayName("Skill Level")]
        [Required(ErrorMessage = "Please Select Your Skill Level")]
        public EnumLevel SkillLevel { get; set; }

        [DisplayName("Is Certificate")]
        public string Iscertificate { get; set; }


        public bool IsActive { get; set; }
    }
}
