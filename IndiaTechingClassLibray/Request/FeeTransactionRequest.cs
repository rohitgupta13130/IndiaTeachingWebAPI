using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace India_Teaching.Request
{
    public class FeeTransactionRequest
    {
        public int FeetransactionId { get; set; }
        public int MonthId { get; set; }
        public int FeeId { get; set; }
        public int StudentId { get; set; }
    }
}