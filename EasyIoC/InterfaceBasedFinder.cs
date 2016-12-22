﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace EasyIoC {
    public class InterfaceBasedFinder : IClassFinder {
        public IEnumerable<Type> FindRegisteredClasses(IEnumerable<Assembly> assemblies)
        {
            return
                from assembly in assemblies
                from type in assembly.DefinedTypes
                where typeof(IAutoRegister).IsAssignableFrom(type.GetType())
                select type.AsType();
        }

        public void RegisterClass(Type type, IServiceContainer container)
        {
            var config = (IAutoRegister)Activator.CreateInstance(type);
            config.RegisterModules(container);
        }
    }
}