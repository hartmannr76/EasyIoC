using System;
using System.Linq;
using System.Reflection;

namespace EasyIoC {
    public class ServiceRegistrar {
        public void RegisterTypeForLifetime(IServiceContainer collection, Type type, ServiceLifetime lifetime) {
            var iface = type.GetInterfaces().FirstOrDefault();

            if(lifetime == ServiceLifetime.Singleton) {
                collection.AddSingleton(iface, type);
            } else if(lifetime == ServiceLifetime.PerRequest) {
                collection.AddRequestScoped(iface, type);
            } else {
                collection.AddTransient(iface, type);
            }
        }
    }
}