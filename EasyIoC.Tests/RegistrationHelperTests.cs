using Moq;
using NUnit.Framework;

namespace EasyIoC.Tests
{
    [TestFixture]
    public class RegistrationHelperTests
    {
        private Mock<IServiceContainer> _mockCollection;

        [SetUp]
        public void Setup()
        {
            _mockCollection = new Mock<IServiceContainer>();
        }

        [Test]
        public void RegisterTypeForLifetime_GivenSingleton_RegistersCorrectly()
        {
            RegistrationHelper.RegisterTypeForLifetime(_mockCollection.Object, typeof(TestableClass), Lifetime.Singleton);

            _mockCollection.Verify(x => x.AddSingleton(typeof(ITestableClass), typeof(TestableClass)), Times.Once);
        }

        [Test]
        public void RegisterTypeForLifetime_GivenTransient_RegistersCorrectly()
        {
            RegistrationHelper.RegisterTypeForLifetime(_mockCollection.Object, typeof(TestableClass), Lifetime.Transient);

            _mockCollection.Verify(x => x.AddTransient(typeof(ITestableClass), typeof(TestableClass)), Times.Once);
        }

        [Test]
        public void RegisterTypeForLifetime_GivenRequestScoped_RegistersCorrectly()
        {
            RegistrationHelper.RegisterTypeForLifetime(_mockCollection.Object, typeof(TestableClass), Lifetime.PerRequest);

            _mockCollection.Verify(x => x.AddRequestScoped(typeof(ITestableClass), typeof(TestableClass)), Times.Once);
        }
    }

    interface ITestableClass {}

    public class TestableClass : ITestableClass {}
}