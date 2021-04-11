using LedgerCore.Domain.Commons;
using LedgerCore.Domain.Models;
using System;
using System.Threading.Tasks;

namespace LedgerCore.Domain.Infras
{
    public interface IRepository

    {

        Task<Ledger> FindLedgerByIdAsync(Guid id);
        Task<Guid> CreateLedgerAsync(Ledger ledger);
        Task<Guid> UpdateLedgerAsync(Ledger ledger);

        Task<Guid> SaveTransactionAsync(Transaction transaction);

        Task<Guid> UpdateTransactionAsync(Transaction transaction);

        Task<bool> DeleteTransactionAsync(Guid Id);

        Task<IPagedEnumareable<Transaction>> GetTransactionsAsync(PaginationParams paginationParams);

        Task<Account> FindAccountByIdAsync(uint id);
        Task<Account> FindAccountByNameAsync(string name);
        Task<uint> CreateAccountAsync(Account account);
        Task<uint> UpdateAccountAsync(Account account);
        Task<IPagedEnumareable<Account>> GetAccountsAsync(PaginationParams paginationParams);

        Task<IPagedEnumareable<Account>> FindAccountsAsync(string filter, PaginationParams paginationParams);

    }
}
