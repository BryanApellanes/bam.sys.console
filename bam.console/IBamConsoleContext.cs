using Bam.Shell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Console
{
    public interface IBamConsoleContext : IBamContext
    {
        IMenuManager MenuManager { get; }

        void AddValidArgument(string name, string? description = null);
        void AddValidArgument(string name, bool allowNull, bool addAcronym = false, string? description = null, string? valueExample = null);
    }
}
