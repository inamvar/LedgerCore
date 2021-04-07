namespace LedgerCore.Domain.Models
{
    public class EntityBase<TKey>
        where TKey : struct
    {
        public TKey Id { get; set; }

    }
}
