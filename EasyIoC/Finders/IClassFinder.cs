using System;
using System.Collections.Generic;
using System.Reflection;

namespace EasyIoC.Finders {
    public interface IClassFinder {
        IEnumerable<Type> FindRegisteredClasses(IEnumerable<Assembly> assemblies);
        void RegisterClass(Type type, IServiceContainer container, string Environment);
    }
}