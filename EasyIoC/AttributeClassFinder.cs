using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using EasyIoC.Attributes;

namespace EasyIoC {
    public class AttributeClassFinder : IClassFinder {
        public IEnumerable<Tuple<Type, ServiceLifetime>> FindRegisteredClasses(IEnumerable<Assembly> assemblies) {      
            return
                from assembly in assemblies
                from type in assembly.DefinedTypes
                let attr = type.GetCustomAttribute<AutoRegisterAttribute>()
                where attr != null
                let lifetime = attr.ServiceLifetime
                select Tuple.Create(type.AsType(), lifetime);
        }
    }
}