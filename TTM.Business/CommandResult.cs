using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTM.Business
{
    public class CommandResult
    {
        private CommandResult()
        {

        }
        private const string _successDefaultMsg = "Action completed successfully.";
        private const string _failDefaultMsg = "Action failed.";

        public bool IsSuccess { get; private set; }
        public string? Message { get; private set; }
        public string? ErrorMessage { get; private set; }
        public string? Token { get; private set; }

        internal static CommandResult Success(string message)
        {
            return new CommandResult
            {
                IsSuccess = true,
                Message = message,
                ErrorMessage = string.Empty
            };
        }

        internal static CommandResult Success(string message, string token)
        {
            return new CommandResult
            {
                IsSuccess = true,
                Message = message,
                ErrorMessage = string.Empty,
                Token = token
            };
        }

        internal static CommandResult Failure(string message)
        {
            return new CommandResult()
            {
                IsSuccess = false,
                Message = message
            };
        }
        internal static CommandResult Error(string message, Exception ex)
        {
            return new CommandResult
            {
                IsSuccess = false,
                Message = message,
                ErrorMessage = ex.ToString()
            };
        }

        internal static CommandResult Success()
        {
            return Success(_successDefaultMsg);
        }
        internal static CommandResult Failure()
        {
            return Failure(_failDefaultMsg);
        }
        internal static CommandResult Error(Exception ex)
        {
            return Error(_failDefaultMsg, ex);
        }

    }
}
