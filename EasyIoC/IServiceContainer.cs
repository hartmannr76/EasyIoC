using System;

namespace EasyIoC {
    public interface IServiceContainer {
        void AddSingleton(Type interfaceToAdd, Type implementation);
        void AddRequestScoped(Type interfaceToAdd, Type implementation);
        void AddTransient(Type interfaceToAdd, Type implementation);
    }
}