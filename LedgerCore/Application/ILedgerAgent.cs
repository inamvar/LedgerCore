using LedgerCore.Domain.Commons;
using LedgerCore.Domain.Models;
using System;
using System.Threading.Tasks;

namespace LedgerCore.Application
{
    public interface ILedgerAgent : IDisposable
    {
        Task<Ledger> CreateNewLedgerAsync(DateTime start, DateTime end, string title);
        Task<ILedgerAgent> SetLedgerByIdAsync(Guid id);
        ILedgerAgent SetLedger(Ledger ledger);
        Task<ILedgerAgent> OpenTransactionAsync(DateTime datetime, Guid ledgerId);
        Task<ILedgerAgent> OpenTransactionAsync(DateTime datetime, string note = "");
        Task<ILedgerAgent> OpenTransactionAsync(DateTime datetime, string note = "", Guid? ledgerId = null);
        Task<ILedgerAgent> AddEntryAsync(decimal amount, Account account, EntryType entryType, string note = "");
        Task<ILedgerAgent> AddEntryAsync(decimal amount, uint accountId, EntryType entryType, string note = "");
        Task<Transaction> CommitTransactionAsync();
        Transaction GetTransaction();
        Ledger GetLedger();
    }
}
