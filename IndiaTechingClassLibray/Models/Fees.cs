using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace India_Teaching.Models
{
    public class Fees 
    {
        public List<Fees> FeesIndex { get; set; }
        public int Id { get; set; }
        public int FeeAmount { get; set; }
        public bool IsActive { get; set; }
    }

}