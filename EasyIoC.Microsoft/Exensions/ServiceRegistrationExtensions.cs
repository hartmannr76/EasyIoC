using Microsoft.Extensions.DependencyInjection;
using EasyIoC.Finders;
using System;
using System.Collections.Generic;

namespace EasyIoC.Microsoft.Exensions {
    public static class ServiceRegistrationExtensions {
		/// <summary>
		/// Registers the first discovered occurrence of 
		/// </summary>
		/// <param name="collection">Service collection</param>
		/// <param name="classFinder">EasyIoC finder</param>
        /// <param name="Environment">Environment to register instances for</param>
        public static void RegisterDependencies(
            this IServiceCollection collection,
            IClassFinder classFinder,
            string Environment = null) {
            var assemblyFinder = new AssemblyFinder();
            var assemblies = assemblyFinder.FindAssemblies(null);

            var classesToRegister = classFinder.FindRegisteredClasses(assemblies);
            var serviceCollection = new ServiceContainer(collection);

            foreach(var item in classesToRegister) {
                classFinder.RegisterClass(item.Type, serviceCollection, Environment);
            }
        }

        public static void RegisterDependencies(
            this IServiceCollection collection,
            IClassFinder classFinder,
            Func<string, bool> canRegister) {
            var assemblyFinder = new AssemblyFinder();
            var assemblies = assemblyFinder.FindAssemblies(null);
            Dictionary<string, bool> registeredResponses = new Dictionary<string, bool>();

            var classesToRegister = classFinder.FindRegisteredClasses(assemblies);
            var serviceCollection = new ServiceContainer(collection);

            foreach (var item in classesToRegister) {
                if (!registeredResponses.ContainsKey(item.Environment)) {
                    var envCanRegister = canRegister(item.Environment);
                    registeredResponses.Add(item.Environment, envCanRegister);
                }

                if (registeredResponses[item.Environment]) {
                    classFinder.RegisterClass(item.Type, serviceCollection, null);
                }
            }
        }
    }
}