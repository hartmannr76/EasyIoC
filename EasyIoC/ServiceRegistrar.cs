using System;
using System.Linq;
using System.Reflection;

namespace EasyIoC {
    public class ServiceRegistrar {
        public void RegisterTypeForLifetime(IServiceContainer collection, Type type, Lifetime lifetime) {
            var iface = type.GetInterfaces().FirstOrDefault();

            if(lifetime == Lifetime.Singleton) {
                collection.AddSingleton(iface, type);
            } else if(lifetime == Lifetime.PerRequest) {
                collection.AddRequestScoped(iface, type);
            } else {
                collection.AddTransient(iface, type);
            }
        }
    }
}