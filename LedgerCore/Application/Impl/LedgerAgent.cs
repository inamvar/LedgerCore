using LedgerCore.Domain.Commons;
using LedgerCore.Domain.Infras;
using LedgerCore.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LedgerCore.Application.Impl
{
    public class LedgerAgent : ILedgerAgent
    {

        private Ledger _ledger;
        private Transaction _transaction;
        private IRepository _repository;
        public LedgerAgent(IRepository repository)
        {
            _repository = repository;


            

        }


        public async Task<Ledger> CreateNewLedgerAsync(DateTime start, DateTime end, string title)
        {
            try
            {
                var l = new Ledger
                {
                    Id = Guid.NewGuid(),
                    Title = title,
                    StartDate = start,
                    EndDate = end,
                    Transactions = new List<Transaction>()
                };
                _ = await _repository.CreateLedgerAsync(l);
                _ledger = l;
                return l;
            }
            catch (LedgerException e)
            {

                //TODO: exception customization and error logging
                throw e;
            }
        }


        public async Task<ILedgerAgent> SetLedgerByIdAsync(Guid id)
        {





            var l = await _repository.FindLedgerByIdAsync(id);
            if (l == null)
            {
                _ledger = null;
                throw new LedgerException($"Ledger <{id}> not found", ErrorCodes.LedgerNotFound);
            }
            else
            {
                _ledger = l;
            }
            return this;
        }

        public ILedgerAgent SetLedger(Ledger ledger)
        {
            if (ledger == null)
            {
                throw new LedgerException($"Ledger is null", ErrorCodes.LedgerIsNull);
            }
            else
            {
                _ledger = ledger;
                return this;
            }
        }


        public ILedgerAgent OpenTransaction(DateTime datetime, Guid ledgerId)
        {
            return  OpenTransaction(datetime, "", ledgerId);
        }

        public ILedgerAgent OpenTransaction(DateTime datetime, string note = "")
        {
            return  OpenTransaction(datetime, note, null);
        }

        public ILedgerAgent OpenTransaction(DateTime datetime, string note = "", Guid? ledgerId = null)
        {

            try
            {
                if (ledgerId.HasValue)
                {
                    _ =  SetLedgerByIdAsync(ledgerId.Value).Result;
                }
                _transaction = new Transaction()
                {
                    Id = Guid.NewGuid(),
                    DateTime = datetime,
                    CreatedAt = DateTime.UtcNow,
                    ModifiedAt = DateTime.UtcNow,
                    Note = note
                };
                _transaction.Entries = new List<Entry>();

            }
            catch (LedgerException e)
            {
                //TODO: exception customization and error logging
                throw e;
            }
            catch (Exception e)
            {
                //TODO: exception customization and error logging
                throw e;
            }
            return this;
        }




        public ILedgerAgent AddEntry(decimal amount, Account account, EntryType entryType, string note = "")
        {
            return   AddEntry(amount, account.Id, entryType, note);
        }

        public  ILedgerAgent AddEntry(decimal amount, uint accountId, EntryType entryType, string note = "")
        {
            if (_transaction != null)
            {
                
                var account =  _repository.FindAccountByIdAsync(accountId).Result;
                if (account == null)
                {
                    throw new LedgerException($"Account <{accountId}> not found", ErrorCodes.AccountNotFound);
                }
                var entry = new Entry
                {
                    Id = Guid.NewGuid(),
                    AccountId = accountId,
                    Amount = amount,
                    CreatedAt = DateTime.UtcNow,
                    ModifiedAt = DateTime.UtcNow,
                    EntryType = entryType,
                    LedgerId = _transaction.LedgerId,
                    TransactionId = _transaction.Id,
                    TransactionDatetime = _transaction.DateTime,
                    Note = note
                };

                _transaction.Entries.Add(entry);

            }
            else
            {
                //TODO: error logging
                throw new LedgerException("Transactoin is null, Please open new Transaction first", ErrorCodes.TransactionIsNull);
            }
            return this;
        }


        public async Task<Transaction> CommitTransactionAsync()
        {

            try
            {
                if (_transaction == null)
                {
                    throw new LedgerException("Transactoin is null, Please open new Transaction first", ErrorCodes.TransactionIsNull);

                }

                if (_transaction.Entries == null || _transaction.Entries.Count() < 2)
                {
                    throw new LedgerException("Transaction shoud have atleast 2 entries", ErrorCodes.TransactionInsuffientEntries);
                }

                var debits = _transaction.Entries.Where(w => w.EntryType == EntryType.DEBIT).Sum(s => s.Amount);
                var credits = _transaction.Entries.Where(w => w.EntryType == EntryType.DEBIT).Sum(s => s.Amount);

                if (debits != credits)
                {
                    throw new LedgerException("Debit and Credit amounts are not equals.", ErrorCodes.TransactionAmountIsNotBalanced);
                }
                _ = await _repository.SaveTransactionAsync(_transaction);
                var returnValue = _transaction;
                _transaction = null;
                return returnValue;
            }
            catch (LedgerException e)
            {
                //TODO: exception customization and error logging
                throw e;
            }
            catch (Exception e)
            {
                //TODO: exception customization and error logging
                throw e;
            }
        }



        public Transaction GetTransaction()
        {
            return _transaction;
        }

        public Ledger GetLedger()
        {
            return _ledger;
        }

        public void Dispose()
        {
            _repository = null;
            _ledger = null;
            _transaction = null;
        }
    }
}
