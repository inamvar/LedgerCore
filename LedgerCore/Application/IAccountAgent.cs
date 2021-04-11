using LedgerCore.Domain.Commons;
using LedgerCore.Domain.Models;
using System;
using System.Threading.Tasks;

namespace LedgerCore.Application
{
    public interface IAccountAgent : IDisposable
    {
        Task<uint> CreateAccountAsync(string accountName, uint accountId);
        Task<uint> CreateAccountAsync(string accountName, uint accountId, uint parentId);
        Task<IPagedEnumareable<Account>> GetAll(int page = 1, int pageSize = 25);
        Task<decimal> GetBalanceAsync(uint accountId, Guid ledgerId);
    }
}
