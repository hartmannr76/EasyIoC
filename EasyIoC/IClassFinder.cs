using System;
using System.Collections.Generic;
using System.Reflection;

namespace EasyIoC {
    public interface IClassFinder {
        IEnumerable<Type> FindRegisteredClasses(IEnumerable<Assembly> assemblies);
        void RegisterClass(Type type, IServiceContainer container);
    }
}