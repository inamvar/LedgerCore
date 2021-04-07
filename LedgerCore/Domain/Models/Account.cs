using LedgerCore.Domain.Commons;
using System;

namespace LedgerCore.Domain.Models
{
    public class Account : EntityBaseStamped<UInt32>
    {

        public string Title { get; set; }
        public string Description { get; set; }
        public AccountType AccountType { get; set; }

        public UInt32? ParentId { get; set; }
        public Account Parent { get; set; }

    }
}
