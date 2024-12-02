using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;


namespace India_Teaching.Models
{
    public class FeeBatches 
    {

        public List<FeeBatches> FeeBatchesIndex { get; set; }

        public int Id { get; set; }

        [DisplayName("Fee")]
        [Required(ErrorMessage = "Please select Fee")]
        public int FeeId { get; set; }

        [DisplayName("Batch")]
        [Required(ErrorMessage = "Please select Batch")]
        public int batchId { get; set; }

        public int FeeAmount { get; set; }

        public string BatchName { get; set; }

        //public IEnumerable<SelectListItem> FeeList { get; set; }

        //public IEnumerable<SelectListItem> BatchList { get; set; }

        public bool IsActive { get; set; }

    }
}