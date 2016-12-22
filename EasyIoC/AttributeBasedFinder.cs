using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using EasyIoC.Attributes;

namespace EasyIoC {
    public class AttributeBasedFinder : IClassFinder
    {
        private readonly ServiceRegistrar _registrar;

        public AttributeBasedFinder()
        {
            _registrar = new ServiceRegistrar();
        }

        public IEnumerable<Type> FindRegisteredClasses(IEnumerable<Assembly> assemblies) {
            return
                from assembly in assemblies
                from type in assembly.DefinedTypes
                let attr = type.GetCustomAttribute<AutoRegisterAttribute>()
                where attr != null
                let lifetime = attr.ServiceLifetime
                select type.AsType();
        }

        public void RegisterClass(Type type, IServiceContainer container)
        {
            var attribute = type.GetTypeInfo().GetCustomAttribute<AutoRegisterAttribute>();
            var lifetime = attribute.ServiceLifetime;
            _registrar.RegisterTypeForLifetime(container, type, lifetime);
        }
    }
}