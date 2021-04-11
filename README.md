# LedgerCore
### A .Net Standard 2.1 library for double entry accounting book keeping. 

** NOTE: I Just stared to writing this library and it not suitable for using in production environment. **

A ledger is a book or collection of accounts in which account transactions are recorded. Each account has an opening or carry-forward balance, would record transactions as either a debit or credit in separate columns and the ending or closing balance.

### Abbreviations used
- Dr – Debit side of a ledger. "Dr" stands for "Debit register"
- Cr – Credit side of a ledger. "Cr" stands for "Credit register"

### Chart of accounts
A chart of accounts is a list of the accounts codes that can be identified with numeric, alphabetical, or alphanumeric codes allowing the account to be located in the general ledger. The equity section of the chart of accounts is based on the fact that the legal structure of the entity is of a particular legal type. Possibilities include sole trader, partnership, trust, and company.

### Usage:
```C#
            // create new repository or inject it from IoC
            // notice: we used  memory repository that is a built in  implementation of IRepository 
            // and its for demo purpose.
            var repo = new InMemoryRepository();

            // create new ledger agent and pass repository to it
            var agent = new LedgerAgent(repo);


            // create a Ledger
            var ledger = new Ledger
            {
                Id = Guid.NewGuid(),
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddYears(1),
                Title = "My Ledger"
            };


            // Assign ledger to Agent 
            agent.SetLedger(ledger);

            
            // open a new tranasction and use chain method `AddEntry()`
            // to add entries and commit the transaction at the end
            // notice: you should find account or account's Id  from IAccountAgent before adding entries:
            var t = await agent.OpenTransaction(datetime: DateTime.Now, note: "Buy a car")
                 .AddEntry( amount: 10000, accountId: 1101, EntryType.DEBIT, note: "some note")
                 .AddEntry( amount: 10000, accountId: 7101, EntryType.CREDIT, note: "some other note")
                 .CommitTransactionAsync();
               

```

## License
All other code in this repository is licensed under a [MIT license](https://github.com/inamvar/LedgerCore/blob/main/LICENSE).
