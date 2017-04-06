using Microsoft.Extensions.DependencyInjection;
using EasyIoC.Finders;

namespace EasyIoC.Microsoft.Exensions {
    public static class ServiceRegistrationExtensions {
		/// <summary>
		/// Registers the first discovered occurrence of 
		/// </summary>
		/// <param name="collection">Collection.</param>
		/// <param name="classFinder">Class finder.</param>
        public static void RegisterDependencies(this IServiceCollection collection, IClassFinder classFinder) {
            var assemblyFinder = new AssemblyFinder();
            var assemblies = assemblyFinder.FindAssemblies(null);

            var classesToRegister = classFinder.FindRegisteredClasses(assemblies);
            var serviceCollection = new ServiceContainer(collection);

            foreach(var item in classesToRegister) {
                classFinder.RegisterClass(item, serviceCollection);
            }
        }

		/// <summary>
		/// Registers the first discovered occurrence of 
		/// </summary>
		/// <param name="collection">Collection.</param>
		/// <param name="classFinder">Class finder.</param>
		public static void RegisterDependencies(
			this IServiceCollection collection,
			IClassFinder classFinder)
		{
			var assemblyFinder = new AssemblyFinder();
			var assemblies = assemblyFinder.FindAssemblies(null);

			var classesToRegister = classFinder.FindRegisteredClasses(assemblies);
			var serviceCollection = new ServiceContainer(collection);

			foreach (var item in classesToRegister)
			{
				classFinder.RegisterClass(item, serviceCollection);
			}
		}
    }
}