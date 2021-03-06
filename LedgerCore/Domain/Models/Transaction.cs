using System;
using System.Collections.Generic;

namespace LedgerCore.Domain.Models
{
    public class Transaction : EntityBaseStamped<Guid>
    {
        public uint Serial { get; set; }
        public DateTime DateTime { get; set; }
        public string Note { get; set; }
        public ICollection<Entry> Entries { get; set; }
        public Guid LedgerId { get; set; }
    }
}
