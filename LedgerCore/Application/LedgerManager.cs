using LedgerCore.Domain.Infras;
using LedgerCore.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LedgerCore.Application
{
    public class LedgerManager : IManager
    {

        private Ledger _ledger;
        private Transaction _transaction;
        private IRepository _repository;


        public async Task<Ledger> CreateNewLedgerAsync(DateTime start, DateTime end)
        {
            try
            {
                var l = new Ledger
                {
                    StartDate = start,
                    EndDate = end,
                    Transactions = new List<Transaction>()
                };
                var id = await _repository.CreateLedgerAsync(l);
                l.Id = id;
                _ledger = l;
                return l;
            }
            catch (Exception e)
            {
                return null;
            }
        }


        public async Task<LedgerManager> SetLedgerAsync(Guid id)
        {
            this._ledger = await  _repository.FindLedgerById(id);
            return this;
        }

        public LedgerManager SetLedger(Ledger ledger)
        {

            this._ledger = ledger;
            return this;
        }


        public LedgerManager OpenNewTransaction(DateTime datetime)
        {
            _transaction = new Transaction()
            {
                DateTime = datetime,
                CreatedAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow
            };
            _transaction.Entries = new List<Entry>();
            return this;
        }


        public LedgerManager AddEntry(Entry entry)
        {
            entry.TransactionDatetime = _transaction.DateTime;

            _transaction.Entries.Add(entry);
            return this;
        }


        public async Task<Transaction> CommitTransaction()
        {
            //TODO: validate transaction entries
            try
            {
                
                var id = await _repository.SaveTransactionAsync(_transaction);
                _transaction.Id = id;
                _ledger.Transactions.Add(_transaction);
                return _transaction;
            }catch(Exception e)
            {
                return null;
            }
        }



   

        public void Dispose()
        {
            _repository = null;
            _ledger = null;
        }
    }
}
