using System;
using System.Collections.Generic;
using System.Reflection;

namespace EasyIoC.Finders {
    public interface IClassFinder {
        IEnumerable<Discoverable> FindRegisteredClasses(IEnumerable<Assembly> assemblies);
        void RegisterClass(Type type, IServiceContainer container, string environment);
    }
}