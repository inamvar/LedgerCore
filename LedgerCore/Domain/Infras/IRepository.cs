using LedgerCore.Domain.Commons;
using LedgerCore.Domain.Models;
using System;
using System.Threading.Tasks;

namespace LedgerCore.Domain.Infras
{
    public interface IRepository

    {

        Task<Ledger> FindLedgerById(Guid id);
        Task<Guid> CreateLedgerAsync(Ledger ledger);
        Task<Guid> UpdateLedgerAsync(Ledger ledger);

        Task<Guid> SaveTransactionAsync(Transaction transaction);

        Task<Guid> UpdateTransactionAsync(Transaction transaction);

        Task<bool> DeleteTransactionAsync(Guid Id);

        Task<IPagedEnumareable<Transaction>> GetTransactionsAsync(PaginationParams paginationParams); 
    }
}
