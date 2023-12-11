using Bam.Net.CoreServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Console
{
    [ConsoleMenu()]
    public class ConsoleMenuContainer : MenuContainer
    {
        public ConsoleMenuContainer(ServiceRegistry serviceRegistry)
            :base()
        {
            this.SetDependencyProvider(serviceRegistry);
        }

        public virtual ServiceRegistry Configure(ServiceRegistry serviceRegistry)
        {
            return serviceRegistry;
        }
    }
}
