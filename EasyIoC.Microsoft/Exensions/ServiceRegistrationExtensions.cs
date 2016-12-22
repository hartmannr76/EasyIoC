using Microsoft.Extensions.DependencyInjection;

namespace EasyIoC.Microsoft.Exensions {
    public static class ServiceRegistrationExtensions {
        public static void AutoRegisterServices(this IServiceCollection collection, IClassFinder classFinder) {
            var assemblyFinder = new AssemblyFinder();
            var assemblies = assemblyFinder.FindAssemblies(null);

            var classesToRegister = classFinder.FindRegisteredClasses(assemblies);
            var serviceCollection = new ServiceContainer(collection);

            foreach(var item in classesToRegister) {
                classFinder.RegisterClass(item, serviceCollection);
            }
        }
    }
}