using System;
using System.Collections.Generic;

namespace LedgerCore.Domain.Models
{
    public class Ledger : EntityBaseStamped<Guid>
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }
        public IEnumerable<Transaction> Transactions { get; set; }
    }
}
