using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Console
{
    public interface ISuccessReporter
    {
        void ReportSuccess(string message);
    }
}
