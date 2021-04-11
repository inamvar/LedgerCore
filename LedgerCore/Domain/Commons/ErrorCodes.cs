namespace LedgerCore.Domain.Commons
{
    public static class ErrorCodes
    {
        public static readonly int UnknownError = 100;
        public static readonly int TransactionInsuffientEntries = 101;
        public static readonly int TransactionIsNull = 102;
        public static readonly int TransactionAmountIsNotBalanced = 103;
        public static readonly int LedgerNotFound = 104;
        public static readonly int LedgerIsNull = 105;
        public static readonly int AccountNotFound = 106;
    }
}
