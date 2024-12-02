using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace India_Teaching.Models
{
    public class Studentbatches
    {
        public List<Studentbatches> lstStudentbatches { get; set; }
        public int Id { get; set; }

        public Student Student { get; set; }

        public Batches Batches { get; set; }

        public Subject Subject { get; set; }

        [DisplayName("Batch Name")]
        [Required(ErrorMessage = "Please Select Batch Name")]
        public int BatchId { get; set; }



        // public List<SelectListItem> lstBatches { get; set; }

        //  public List<SelectListItem> lstStudent { get; set; }

        public int StudentId { get; set; }

        public string StudentFirstName { get; set; }

        public int batchId { get; set; }

        public bool IsActive { get; set; }
    }
}