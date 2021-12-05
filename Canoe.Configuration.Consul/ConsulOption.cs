using System;
using System.Collections.Generic;
using System.Text;

namespace Canoe.Configuration.Consul
{
    /// <summary>
    /// Consul Options
    /// </summary>
    public class ConsulOption
    {
        /// <summary>
        /// Service Name
        /// </summary>
        public string ServiceName { get; set; }

        /// <summary>
        /// Service IP
        /// </summary>
        public string ServiceIP { get; set; }

        /// <summary>
        /// Service Port
        /// </summary>
        public int ServicePort { get; set; }

        /// <summary>
        /// Service HealthCheck Address
        /// </summary>
        public string ServiceHealthCheck { get; set; }

        /// <summary>
        /// Consul Address
        /// </summary>
        public string Address { get; set; }
    }
}
