using Bam.Shell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Console
{
    public class InputCommandResult : IInputCommandResult
    {
        public InputCommandResult() 
        {
        }

        public string InputName { get; set; }

        object? _invocationResult;
        public object? InvocationResult 
        {
            get
            {
                return _invocationResult;
            }
            set
            {
                _invocationResult = value;
                if(_invocationResult is InputCommandResults results)
                {
                    this.CheckResultsExceptions(results);
                }else if (_invocationResult is InputCommandResult result)
                {
                    this.CheckResultException(result);
                }
            }
        }

        public bool Success
        {
            get
            {
                return Exception == null;
            }
        }

        public string? Message
        {
            get
            {
                return Exception?.Message;
            }
        }

        public Exception Exception
        {
            get;
            set;
        }

        private void CheckResultsExceptions(InputCommandResults results)
        {
            List<Exception> exceptions = new List<Exception>();
            foreach(IInputCommandResult result in results.Results)
            {
                if(!result.Success)
                {
                    exceptions.Add(result.Exception);
                }
            }
            if (exceptions.Count > 0)
            {
                this.Exception = new AggregateException(exceptions);
            }
        }

        private void CheckResultException(InputCommandResult result)
        {
            if (!result.Success)
            {
                this.Exception = result.Exception;
            }
        }
    }
}
