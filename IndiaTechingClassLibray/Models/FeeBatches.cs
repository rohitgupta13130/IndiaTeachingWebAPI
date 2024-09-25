using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace India_Teaching.Models
{
    public class FeeBatches 
    {

        public List<FeeBatches> FeeBatchesIndex { get; set; }

        public int Id { get; set; }
        public int FeeId { get; set; }

        public int batchId { get; set; }

        public int FeeAmount { get; set; }

        //public IEnumerable<SelectListItem> FeeList { get; set; }

        //public IEnumerable<SelectListItem> BatchList { get; set; }

        //public Fees Fees { get; set; }

        public Batches Batches { get; set; }

        public bool IsActive { get; set; }

    }
}