using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyModel;

namespace EasyIoC {
    public class AssemblyFinder {
        private readonly HashSet<string> _defaultIgnoredAssemblies = new HashSet<string>() { "Microsoft.", "System." };

        public ImmutableList<Assembly> FindAssemblies(HashSet<string> ignoredAssemblies) {
            var ctx = DependencyContext.Default;
            ignoredAssemblies = ignoredAssemblies ?? _defaultIgnoredAssemblies;
            
            var assemblyNames = from lib in ctx.RuntimeLibraries
                                from assemblyName in lib.GetDefaultAssemblyNames(ctx)
                                where ignoredAssemblies.Any(x => assemblyName.Name.StartsWith(x))
                                select assemblyName;
            var lookup = assemblyNames.ToLookup(x => x.Name).Select(x => x.First());
            var asList = lookup.Select(Assembly.Load).ToImmutableList();
            return asList;
        }
    }
}