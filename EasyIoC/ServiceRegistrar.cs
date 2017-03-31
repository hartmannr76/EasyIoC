using System;
using System.Linq;
using System.Reflection;

namespace EasyIoC {
    public class ServiceRegistrar {
        public void RegisterTypeForLifetime(IServiceContainer collection, Type type, DependencyLifetime lifetime) {
            var iface = type.GetInterfaces().FirstOrDefault();

            if(lifetime == DependencyLifetime.Singleton) {
                collection.AddSingleton(iface, type);
            } else if(lifetime == DependencyLifetime.PerRequest) {
                collection.AddRequestScoped(iface, type);
            } else {
                collection.AddTransient(iface, type);
            }
        }
    }
}