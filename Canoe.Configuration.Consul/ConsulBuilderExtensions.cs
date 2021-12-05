using Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using System;

namespace Canoe.Configuration.Consul
{
    public static class ConsulBuilderExtensions
    {
        // Implement Consul Middleware
        [Obsolete]
        public static IApplicationBuilder RegisterConsul(this IApplicationBuilder app, IApplicationLifetime lifetime, ConsulOption consulOption)
        {
            var consulClient = new ConsulClient(x =>
            {
                // consul service address
                x.Address = new Uri(consulOption.Address);
            });

            var registration = new AgentServiceRegistration()
            {
                ID = Guid.NewGuid().ToString(),
                Name = consulOption.ServiceName,
                Address = consulOption.ServiceIP, 
                Port = consulOption.ServicePort, 
                Check = new AgentServiceCheck()
                {
                    DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(5),
                    Interval = TimeSpan.FromSeconds(10),
                    HTTP = consulOption.ServiceHealthCheck,
                    Timeout = TimeSpan.FromSeconds(5)
                }
            };
            // Service Rego
            consulClient.Agent.ServiceRegister(registration).Wait();

            // Deregister Service when application stopping
            lifetime.ApplicationStopping.Register(() =>
            {
                consulClient.Agent.ServiceDeregister(registration.ID).Wait();
            });
            return app;
        }
    }
}
