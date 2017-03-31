using System;

namespace EasyIoC.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class DependencyAttribute : Attribute
    {
        public DependencyAttribute(DependencyLifetime lifetime = DependencyLifetime.Transient) {
            DependencyLifetime = lifetime;
        }

        public DependencyLifetime DependencyLifetime;
    }
}
