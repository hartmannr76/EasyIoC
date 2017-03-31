using System;

namespace EasyIoC
{
    public enum DependencyLifetime
    {
        Transient,
        Singleton,
        PerRequest
    }
}
