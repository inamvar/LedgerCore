using LedgerCore.Domain.Commons;
using LedgerCore.Domain.Infras;
using LedgerCore.Domain.Models;
using System;
using System.Threading.Tasks;

namespace LedgerCore.Application.Impl
{
    public class AccountAgent : IAccountAgent
    {

        private IRepository _repository;
        public AccountAgent(IRepository repository)
        {
            _repository = repository;
        }


        public Task<uint> CreateAccountAsync(string accountName, uint accountId)
        {
            throw new NotImplementedException();
        }

        public Task<uint> CreateAccountAsync(string accountName, uint accountId, uint parentId)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            
        }


        public Task<IPagedEnumareable<Account>> GetAll(int page = 1, int pageSize = 25)
        {
            throw new NotImplementedException();
        }

        public async Task<decimal> GetBalanceAsync(uint accountId, Guid ledgerId)
        {
            return await _repository.GetAccountBalanceAsync(accountId, ledgerId);
        }
    }
}
