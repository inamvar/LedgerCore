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
        ILedgerAgent OpenTransaction(DateTime datetime, Guid ledgerId);
        ILedgerAgent OpenTransaction(DateTime datetime, string note = "");
        ILedgerAgent OpenTransaction(DateTime datetime, string note = "", Guid? ledgerId = null);
        ILedgerAgent AddEntry(decimal amount, Account account, EntryType entryType, string note = "");
        ILedgerAgent AddEntry(decimal amount, uint accountId, EntryType entryType, string note = "");
        Task<Transaction> CommitTransactionAsync();
        Transaction GetTransaction();
        Ledger GetLedger();
    }
}
