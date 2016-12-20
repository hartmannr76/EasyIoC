using System;
using System.Reflection;

namespace EasyIoC {
    public class ServiceRegistrar {
        public void RegisterTypeForLifetime(IServiceContainer collection, Type type, ServiceLifetime lifetime) {
            var interfaces = type.GetInterfaces();
            
            foreach(var face in interfaces) {
                if(lifetime == ServiceLifetime.Singleton) {
                    collection.AddSingleton(face, type);
                } else if(lifetime == ServiceLifetime.PerRequest) {
                    collection.AddRequestScoped(face, type);
                } else {
                    collection.AddTransient(face, type);
                }
            }
        }
    }
}