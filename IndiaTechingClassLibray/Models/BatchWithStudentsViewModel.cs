using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace India_Teaching.Models
{
    public class BatchWithStudentsViewModel
    {
        
        public Batches Batch { get; set; }

        public List<Student> Students { get; set; }
    }
}