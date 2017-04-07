using System;

namespace EasyIoC.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class DependencyAttribute : Attribute
    {
        public DependencyAttribute(Lifetime lifetime = Lifetime.Transient) {
            Lifetime = lifetime;
        }

        public Lifetime Lifetime;
    }
}
