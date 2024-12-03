using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace India_Teaching.Models
{
    public class Notes 
    {
        public List<Notes> NotesIndex { get; set; }

        public int Id { get; set; }

        [DisplayName("File")]
        [Required(ErrorMessage = "Please provide a link for the text file")]
        public string LinkfortextFile { get; set; }

        [DisplayName("Title")]
        [Required(ErrorMessage = "Please provide a title")]
        [StringLength(100, ErrorMessage = "Title can't be longer than 100 characters")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Teacher Id is required")]
        public int Teacherid { get; set; }

        public bool IsActive { get; set; }
    }
}