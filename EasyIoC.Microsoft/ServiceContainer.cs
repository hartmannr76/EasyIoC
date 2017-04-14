using System;
using Microsoft.Extensions.DependencyInjection;

namespace EasyIoC.Microsoft {
    public class ServiceContainer : IServiceContainer {
        private readonly IServiceCollection _container;

        public ServiceContainer(IServiceCollection container) {
            _container = container;
        }

        public void AddRequestScoped(Type interfaceToAdd, Type implementation)
        {
			if (!_container.Contains(
				new ServiceDescriptor(interfaceToAdd, implementation, ServiceLifetime.Scoped))) {
				_container.AddScoped(interfaceToAdd, implementation);
			}
        }

        public void AddSingleton(Type interfaceToAdd, Type implementation)
        {
			if (!_container.Contains(
				new ServiceDescriptor(interfaceToAdd, implementation, ServiceLifetime.Singleton)))
			{
				_container.AddSingleton(interfaceToAdd, implementation);
			}
        }

        public void AddTransient(Type interfaceToAdd, Type implementation)
		{
			if (!_container.Contains(
				new ServiceDescriptor(interfaceToAdd, implementation, ServiceLifetime.Transient)))
			{
				_container.AddTransient(interfaceToAdd, implementation);
			}
        }
    }
}