using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace India_Teaching.Models
{
    public class HomeWork 
    {
        public List<HomeWork> HomeworkIndex { get; set; }

        public int Id { get; set; }
        public int ClassId { get; set; }
       // public IEnumerable<SelectListItem> ClassList { get; set; }
        public int SubjectId { get; set; }
       // public IEnumerable<SelectListItem> SubjectList { get; set; }
        public int TeacherId { get; set; }
      //  public IEnumerable<SelectListItem> TeacherList { get; set; }
        public string Homework { get; set; }

        public string ClassName { get; set; }
        public string SubjectName { get; set; }
        public string TeacherName { get; set; }

        
        
        

    }
}