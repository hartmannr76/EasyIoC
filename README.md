# EasyIoC
Travis: ![Travis](https://api.travis-ci.org/hartmannr76/EasyIoC.svg?branch=master)  
NuGet - EasyIoC: ![EasyIoC](https://img.shields.io/nuget/v/EasyIoC.svg)  
NuGet - EasyIoC.Microsoft: ![EasyIoC.Microsoft](https://img.shields.io/nuget/v/EasyIoC.Microsoft.svg) 

Dependency registration abstraction for popular IoC frameworks.

## Why?
Inversion of Control (IoC) and Dependency Injection (DI)
are popular concepts implemented in .NET. So much so, that
Microsoft even added their own native version of it in .NET core.

Many container frameworks have come out as replacements for
this for varying reasons, but implementations around using those
containers are all relatively the same.

This **IS NOT** a DI container. This library utilizes concepts
that I've seen as common practices around using those containers.
Many people will just bake these into their application, but I
wanted a friendly way of using those same concepts for multiple
projects.

## Usage

### Attribute Registration
One of these patterns for dependency registration of services involve
adding an attribute to your implementation that allows the system
to discover which services you want to be added.

In your `Startup.cs` (in this case I will be using .NET native DI
library), you will need the following:

```c#
using EasyIoC;
using EasyIoC.Microsoft.Extensions;


    public void ConfigureServices(IServiceCollection services)
    {
        ...

        services.AutoRegisterServices(new AttributeBasedFinder());
        
        ...
    }
```

The `EasyIoC.Microsoft.Extensions` namespace provides an
extension to `IServiceCollection`. The function `AutoRegisterServices`
accepts an `IClassFinder` that is used to determine how to find
the classes to register. In this case, we are using an "Attribute"
finder. To have your implementation class be automatically 
discovered, you simply need to decorate it with the proper
attribute, and provide a lifetime to it.

Your `FooBarService` now becomes:

`FooBarService.cs`:
```c#
using EasyIoC;
using EasyIoC.Attributes;

[Dependency(Lifetime.Singleton)]
public class FooBarService : IFooBarService {
    public void DoSomething() {
        Console.WriteLine("FooBar");
    }
}
```

The lifetimes are limited to `Singleton`, `Transient`, and 
`PerRequest`; which are common lifetime scopes in many DI
container frameworks.

### Multiple Registering Classes
Another construct that I've seen is the use of multiple classes
responsible for registering all services. These can be a 
single class per solution, or however you may see fit. An
example of what this may look like is:

`Startup.cs`:
```c#
using EasyIoC;
using EasyIoC.Microsoft.Extensions;


    public void ConfigureServices(IServiceCollection services)
    {
        ...

        services.AutoRegisterServices(new InterfaceBasedFinder());
        
        ...
    }
```

`Bootstrapper.cs`:
```c#
using EasyIoC;

public class FooBarService : IDependencyRegisrar {
    public FooBarService() {} // There must be an empty constructor

    public void RegisterModules(IServiceContainer container) {
        container.AddRequestScoped(IFooService, FooService);
        container.AddSingleton(IBarService, BarService);
        container.AddTransientService(IFooBarService, FooBarService);
    }
}
```

You can add as many instances of the `IDependencyRegisrar` as you want
if you want your classes to be kept inside your solution.

### Supported Container Frameworks
- [X] Microsoft
- [ ] Autofac
- [ ] Ninject