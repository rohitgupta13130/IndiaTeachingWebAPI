using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndiaTechingClassLibray.Models
{
    public class Skill
    {
        public List<Skill> SkillIndex { get; set; }

        [Required(ErrorMessage = "Please Enter Correct Id")]
        public int SkillId { get; set; }

        [Required(ErrorMessage = "Please Enter Your Name")]
        [StringLength(50, ErrorMessage = "Skill Name can't be longer than 50 characters")]
        [RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "Special characters are not allowed")]
        public string SkillName { get; set; }

        [Required(ErrorMessage = "Please Enter Your Skill Level")]
        [StringLength(50, ErrorMessage = "Skill Level can't be longer than 50 characters")]
        [RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "Special characters are not allowed")]
        public string SkillLevel { get; set; }

        [Required(ErrorMessage = "Please specify if the skill is certified")]
        public string Iscertificate { get; set; }

        public bool IsActive { get; set; }
    }
}
