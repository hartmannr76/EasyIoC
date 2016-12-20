using Microsoft.Extensions.DependencyInjection;

namespace EasyIoC.Microsoft.Exensions {
    public static class ServiceRegistrationExtensions {
        public static void AutoRegisterServices(this IServiceCollection collection, IClassFinder classFinder) {
            var assemblyFinder = new AssemblyFinder();
            var assemblies = assemblyFinder.FindAssemblies(null);

            var classesToRegister = classFinder.FindRegisteredClasses(assemblies);
            var serviceRegistrar = new ServiceRegistrar();
            var serviceCollection = new ServiceContainer(collection);

            foreach(var item in classesToRegister) {
                serviceRegistrar.RegisterTypeForLifetime(serviceCollection, item.Item1, item.Item2);
            }
        }
    }
}