using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace India_Teaching.Models
{
    public class FeeTransaction
    {
        public int FeetransactionId { get; set; }

        [Required(ErrorMessage = "Month Id is required")]
        public int MonthId { get; set; }

        public int Year { get; set; }

        [Required(ErrorMessage = "Fee Id is required")]
        public int FeeId { get; set; }

        [Required(ErrorMessage = "Student Id is required")]
        public int StudentId { get; set; }

        public Studentbatches Studentbatches { get; set; }

        public string MonthsName { get; set; }

        public int PreBalanceAmount { get; set; }

        public int FeeAmount { get; set; }

        public int PaidAmount { get; set; }

        public int BalanceAmount { get; set; }

        public DateTime EntryDate { get; set; }

        public string UserName { get; set; }

        public int UserId { get; set; }

        public List<FeeTransaction> feeTransactionsIndex { get; set; }

        public List<FeeTransaction> feeTransactionsToAdd { get; set; }

        public bool toAdd { get; set; } = false;

        public int BatchId { get; set; }
    }
}