using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using EasyIoC.Attributes;

namespace EasyIoC.Finders {
    public class AttributeBasedFinder : IClassFinder
    {
        private readonly ServiceRegistrar _registrar;

        public AttributeBasedFinder()
        {
            _registrar = new ServiceRegistrar();
        }

        public IEnumerable<Discoverable> FindRegisteredClasses(IEnumerable<Assembly> assemblies) {
            var allAttributedClasses = from assembly in assemblies
                from type in assembly.DefinedTypes
                let attr = type.GetCustomAttribute<DependencyAttribute>()
                where attr != null
                select new Discoverable { Type = type.AsType(), Environment = attr.Environment };
            
            // Returns environment based implementations first
            foreach (var foundClass in allAttributedClasses) {
				if (foundClass.Environment != null) {
					yield return foundClass;
                }
            }

            // Returns implementations that can be registered for any environment.
            // If there is an environment specific one, the "any" version will be ignored
            foreach (var foundClass in allAttributedClasses) {
				if (foundClass.Environment == null) {
					yield return foundClass;
                }
            }
        }

        public void RegisterClass(Type type, IServiceContainer container, string environment)
        {
            var attribute = type.GetTypeInfo().GetCustomAttribute<DependencyAttribute>();
            var lifetime = attribute.Lifetime;
            var attrEnvironment = attribute.Environment;

            if (environment == null || attrEnvironment == environment) {
                _registrar.RegisterTypeForLifetime(container, type, lifetime);
            }
        }
    }
}