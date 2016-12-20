using System;

namespace EasyIoC.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class AutoRegisterAttribute : Attribute
    {
        public AutoRegisterAttribute(ServiceLifetime lifetime = ServiceLifetime.Transient) {
            this.ServiceLifetime = lifetime;
        }
        public ServiceLifetime ServiceLifetime = ServiceLifetime.Transient;
    }
}
