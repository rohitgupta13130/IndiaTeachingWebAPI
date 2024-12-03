using India_Teaching.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace India_Teaching.Request
{
    public class StudentbatchesRequest
    {
        public int Id { get; set; }

        public Student Student { get; set; }

        public Batches Batches { get; set; }

        public Subject Subject { get; set; }

     //   public List<SelectListItem> lstBatches { get; set; }

    //    public List<SelectListItem> lstStudent { get; set; }

        public int BatchId { get; set; }

        public int StudentId { get; set; }

        public string FirstName { get; set; }
    }
}