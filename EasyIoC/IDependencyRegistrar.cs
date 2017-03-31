namespace EasyIoC
{
    public interface IDependencyRegistrar
    {
        void RegisterDependencies(IServiceContainer container);
    }
}