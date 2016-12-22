using Moq;
using NUnit.Framework;

namespace EasyIoC.Tests
{
    [TestFixture]
    public class ServiceRegistrarTests
    {
        private ServiceRegistrar _service;
        private Mock<IServiceContainer> _mockCollection;

        [SetUp]
        public void Setup()
        {
            _service = new ServiceRegistrar();
            _mockCollection = new Mock<IServiceContainer>();
        }

        [Test]
        public void RegisterTypeForLifetime_GivenSingleton_RegistersCorrectly()
        {
            _service.RegisterTypeForLifetime(_mockCollection.Object, typeof(TestableClass), ServiceLifetime.Singleton);

            _mockCollection.Verify(x => x.AddSingleton(typeof(ITestableClass), typeof(TestableClass)), Times.Once);
        }

        [Test]
        public void RegisterTypeForLifetime_GivenTransient_RegistersCorrectly()
        {
            _service.RegisterTypeForLifetime(_mockCollection.Object, typeof(TestableClass), ServiceLifetime.Transient);

            _mockCollection.Verify(x => x.AddTransient(typeof(ITestableClass), typeof(TestableClass)), Times.Once);
        }

        [Test]
        public void RegisterTypeForLifetime_GivenRequestScoped_RegistersCorrectly()
        {
            _service.RegisterTypeForLifetime(_mockCollection.Object, typeof(TestableClass), ServiceLifetime.PerRequest);

            _mockCollection.Verify(x => x.AddRequestScoped(typeof(ITestableClass), typeof(TestableClass)), Times.Once);
        }
    }

    interface ITestableClass {}

    public class TestableClass : ITestableClass {}
}