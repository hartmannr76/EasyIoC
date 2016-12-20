using System;
using System.Collections.Generic;
using System.Reflection;

namespace EasyIoC {
    public interface IClassFinder {
        IEnumerable<Tuple<Type, ServiceLifetime>> FindRegisteredClasses(IEnumerable<Assembly> assemblies);
    }
}