using System;

namespace LedgerCore.Domain.Commons
{
    public class LedgerException : Exception
    {
        public LedgerException(string message) : base(message)
        {
            ErrorCode = ErrorCodes.UnknownError;
        }

        public LedgerException(string message, int errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }


        public int ErrorCode { get; set; }
    }
}
