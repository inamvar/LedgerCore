using LedgerCore.Domain.Infras;
using System;

namespace LedgerCore.Domain.Models
{
    public class EntityBaseStamped<TKey> : EntityBase<TKey>, IHasDateTimeStamp
        where TKey : struct
    {
        public DateTime? CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
