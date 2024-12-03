using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace India_Teaching.Request
{
    public class NotesRequest
    {
        public int Id { get; set; }
        [DisplayName("File")]
        public string LinkfortextFile { get; set; }
        [DisplayName("Title")]
        public string Teacherid { get; set; }

        public string Title { get; set; }
    }
}