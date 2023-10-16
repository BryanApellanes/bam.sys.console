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

        public object? InvocationResult { get; set; }

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
    }
}
