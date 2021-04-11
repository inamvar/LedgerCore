using LedgerCore.Domain.Commons;
using LedgerCore.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LedgerCore.Domain.Infras
{




    public class InMemoryRepository : IRepository
    {
        private ICollection<Account> _accounts;

        private ICollection<Ledger> _ledgers;


        public InMemoryRepository()
        {
            _accounts = new List<Account>();
            _ledgers = new List<Ledger>();
        }

        public Task<uint> CreateAccountAsync(Account account)
        {
            _accounts.Add(account);
            return Task.FromResult(account.Id);
        }

        public Task<Guid> CreateLedgerAsync(Ledger ledger)
        {

            _ledgers.Add(ledger);
            return Task.FromResult(ledger.Id);
        }

        public Task<bool> DeleteTransactionAsync(Guid ledgerId, Guid Id)
        {
            throw new NotImplementedException();
        }

        public Task<Account> FindAccountByIdAsync(uint id)
        {
            return Task.FromResult(_accounts.First(q => q.Id == id));
        }

        public Task<Account> FindAccountByNameAsync(string name)
        {
            return Task.FromResult(_accounts.First(q => q.Title.Equals(name)));
        }

        public Task<IPagedEnumareable<Account>> FindAccountsAsync(string filter, PaginationParams paginationParams)
        {
            IPagedEnumareable<Account> result;
            var query = _accounts.Where(q => q.Title.Contains(filter));

            result = query.ToPagedList<Account>(paginationParams);

            return Task.FromResult(result);

        }

        public Task<Ledger> FindLedgerByIdAsync(Guid id)
        {
            return Task.FromResult(_ledgers.First<Ledger>(q => q.Id == id));
        }

        public Task<IPagedEnumareable<Account>> GetAccountsAsync(PaginationParams paginationParams)
        {
            return Task.FromResult(_accounts.ToPagedList(paginationParams));
        }

        public Task<IPagedEnumareable<Transaction>> GetTransactionsAsync(Guid ledgerId, PaginationParams paginationParams)
        {
            var ledger = _ledgers.First(q => q.Id == ledgerId);
            if (ledger != null && ledger.Transactions != null)
            {
                return Task.FromResult(ledger.Transactions.ToPagedList(paginationParams));
            }
            else
            {
                return Task.FromResult<IPagedEnumareable<Transaction>>(null);
            }
   
        }

        public Task<Guid> SaveTransactionAsync(Transaction transaction)
        {
            var ledger = _ledgers.First(q => q.Id == transaction.LedgerId);
            if (ledger != null)
            {
                if(ledger.Transactions == null)
                {
                    ledger.Transactions = new List<Transaction>();
                }

                ledger.Transactions.Add(transaction);
              
            }
            return Task.FromResult(transaction.Id);
        }

        public Task<uint> UpdateAccountAsync(Account account)
        {
            throw new NotImplementedException();
        }

        public Task<Guid> UpdateLedgerAsync(Ledger ledger)
        {
            throw new NotImplementedException();
        }

        public Task<Guid> UpdateTransactionAsync(Transaction transaction)
        {
            throw new NotImplementedException();
        }
    }


    public static class ListExtensions
    {
        public static IPagedEnumareable<T> ToPagedList<T>(this IEnumerable<T> query, PaginationParams paginationParams) where T : class

        {
            IPagedEnumareable<T> result;
            result = query
               .Skip(paginationParams.PageSize * (paginationParams.Page - 1))
               .Take(paginationParams.PageSize).ToList<T>() as IPagedEnumareable<T>;

            result.TotalRecords = query.Count();
            result.TotalPages = Convert.ToInt32(result.TotalRecords / paginationParams.PageSize);
            result.PageSize = paginationParams.PageSize;
            result.CurrentPage = paginationParams.Page;

            return result;
        }
    }
}
