using System;
using System.Collections.Generic;

namespace LedgerCore.Domain.Models
{
    public class Transaction : EntityBaseStamped<Guid>
    {
        public UInt32 Serial { get; set; }
        public DateTime DateTime { get; set; }
        public string Note { get; set; }
        public IEnumerable<Entry> Entries { get; set; }
    }
}
