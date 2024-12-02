
using India_Teaching.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace India_Teaching.Models
{
    public class Classes 
    {
        public List<Classes> ClassesIndex { get; set; }
        public int ClassId { get; set; }

        [Required(ErrorMessage = "Please enter the class name.")]
        [RegularExpression(@"^[IVXLCDM\s]+$", ErrorMessage = "Class name should contain only Roman numeral characters.")]
        public string ClassName { get; set; }


        [DisplayName("Section")]
        [Required(ErrorMessage = "Please Select the Section.")]
        public int SectionId { get; set; }



        [DisplayName("Section")]
        public EnumSection SectionName { get; set; }
    }
}