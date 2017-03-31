using System;

namespace EasyIoC
{
    public static class Environments {
        public const string Any = "any";
        public const string Production = "Production";
        public const string Development = "Development";
        public const string CI = "CI";
    }

    public enum DependencyLifetime
    {
        Transient,
        Singleton,
        PerRequest
    }
}
