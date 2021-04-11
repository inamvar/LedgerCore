using LedgerCore.Domain.Commons;
using System;

namespace LedgerCore.Domain.Models
{
    public class Entry : EntityBaseStamped<Guid>
    {

        public uint AccountId { get; set; }
        public Account Account { get; set; }
        public decimal Amount { get; set; }
        public EntryType EntryType { get; set; }
        public Guid TransactionId { get; set; }
        public Transaction Transaction { get; set; }
        public DateTime TransactionDatetime { get; set; }
        public string Note { get; set; }

        public Guid LedgerId { get; set; }
    }
}
