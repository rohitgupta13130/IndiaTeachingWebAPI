
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace India_Teaching.Request
{
    public class ClassRequest
    {
        public int ClassId { get; set; }

        public string ClassName { get; set; }

        // [DisplayName("Section")]
        //public EnumSection SectionId { get; set; }
    }
}