
using India_Teaching.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;


namespace India_Teaching.Models
{
    public class Classes 
    {
        public List<Classes> ClassesIndex { get; set; }
        public int ClassId { get; set; }

        public string ClassName { get; set; }

        public int SectionId { get; set; }

        [DisplayName("Section")]
        public EnumSection SectionName { get; set; }
    }
}