using System;

namespace LedgerCore.Domain.Infras
{
    public interface IHasDateTimeStamp
    {
        public DateTime? CreatedAt { get ; set; }
        public DateTime? ModifiedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
