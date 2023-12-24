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
            this.SetDependencyProvider(this.Configure(serviceRegistry));
        }

        /// <summary>
        /// Configures the specified service registry before setting as the DependencyProvider property.
        /// </summary>
        /// <param name="serviceRegistry">The service registry.</param>
        /// <returns>ServiceRegistry</returns>
        public virtual ServiceRegistry Configure(ServiceRegistry serviceRegistry)
        {
            return serviceRegistry;
        }
    }
}
