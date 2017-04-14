using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace EasyIoC.Finders {
    public class InterfaceBasedFinder : IClassFinder {
        public IEnumerable<Discoverable> FindRegisteredClasses(IEnumerable<Assembly> assemblies)
        {
            return
                from assembly in assemblies
                from type in assembly.DefinedTypes
                where typeof(IDependencyRegistrar).IsAssignableFrom(type.GetType())
                select new Discoverable { Type = type.AsType() };
        }

        public void RegisterClass(Type type, IServiceContainer container, string environment)
        {
            var config = (IDependencyRegistrar)Activator.CreateInstance(type);
            config.RegisterDependencies(container, environment);
        }
    }
}