using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using EasyIoC.Attributes;
using EasyIoC.Finders;
using Moq;
using NUnit.Framework;

namespace EasyIoC.Tests
{
    static class EnvironmentTestHelper {
        public const string ExpectedEnvironment = "Foobar";
    }

    [TestFixture]
    public class AttributeBasedFinderTests
    {
        private AttributeBasedFinder _service;
        private List<Assembly> _mockAssemblies;
        private List<Type> _readOrder;

        [SetUp]
        public void Setup()
        {
            _service = new AttributeBasedFinder();
            _mockAssemblies = null;
            _readOrder = new List<Type>();
        }

        [Test]
        public void FindRegisteredClasses_GivenClassWithAttribute_ReturnsType()
        {
            GivenAssembliesHasType(typeof(TestableWithAttributeClass));

            var result = _service.FindRegisteredClasses(_mockAssemblies);

            Assert.AreEqual(1, result.Count());
            Assert.AreEqual(typeof(TestableWithAttributeClass), result.First().Type);
            Assert.IsNull(result.First().Environment);
        }

        [Test]
        public void FindRegisteredClasses_GivenClassWithAttributeAndEnvironment_ReturnsType()
        {
            GivenAssembliesHasType(typeof(TestableWithAttributeAndEnvironmentClass));

            var result = _service.FindRegisteredClasses(_mockAssemblies);

            Assert.AreEqual(1, result.Count());
            Assert.AreEqual(typeof(TestableWithAttributeAndEnvironmentClass), result.First().Type);
            Assert.AreEqual(EnvironmentTestHelper.ExpectedEnvironment, result.First().Environment);
        }

        [Test]
        public void FindRegisteredClasses_EnvironmentClassFirst_IsDiscoveredFirst()
        {
            GivenAssembliesHasType(typeof(TestableWithAttributeAndEnvironmentClass));
            GivenAssembliesHasType(typeof(TestableWithAttributeClass));

            var result = _service.FindRegisteredClasses(_mockAssemblies);

            Assert.AreEqual(2, result.Count());
            Assert.AreEqual(typeof(TestableWithAttributeAndEnvironmentClass), result.First().Type);
            Assert.AreEqual(EnvironmentTestHelper.ExpectedEnvironment, result.First().Environment);

            Assert.AreEqual(typeof(TestableWithAttributeClass), result.Last().Type);
            Assert.IsNull(result.Last().Environment);

            Assert.AreEqual(typeof(TestableWithAttributeAndEnvironmentClass), _readOrder.First());
            Assert.AreEqual(typeof(TestableWithAttributeClass), _readOrder.Last());
        }

        [Test]
        public void FindRegisteredClasses_EnvironmentClassLast_IsDiscoveredFirst()
        {
            GivenAssembliesHasType(typeof(TestableWithAttributeClass));
            GivenAssembliesHasType(typeof(TestableWithAttributeAndEnvironmentClass));

            var result = _service.FindRegisteredClasses(_mockAssemblies);

            Assert.AreEqual(2, result.Count());
            Assert.AreEqual(typeof(TestableWithAttributeAndEnvironmentClass), result.First().Type);
            Assert.AreEqual(EnvironmentTestHelper.ExpectedEnvironment, result.First().Environment);

            Assert.AreEqual(typeof(TestableWithAttributeClass), result.Last().Type);
            Assert.IsNull(result.Last().Environment);

            Assert.AreEqual(typeof(TestableWithAttributeClass), _readOrder.First());
            Assert.AreEqual(typeof(TestableWithAttributeAndEnvironmentClass), _readOrder.Last());
        }

        [Test]
        public void FindRegisteredClasses_GivenClassWithNoAttribute_ReturnsNoType()
        {
            GivenAssembliesHasType(typeof(TestableWithoutAttributeClass));

            var result = _service.FindRegisteredClasses(_mockAssemblies);

            Assert.AreEqual(0, result.Count());
        }

        private void GivenAssembliesHasType(Type type)
        {
            var assemblyList = _mockAssemblies ?? new List<Assembly>();

            var mockAssembly = new Mock<Assembly>();
            mockAssembly.SetupGet(x => x.DefinedTypes)
                .Callback(() => _readOrder.Add(type))
                .Returns(() => new[] {type.GetTypeInfo()});

            assemblyList.Add(mockAssembly.Object);

            _mockAssemblies = assemblyList;
        }
    }

    [Dependency]
    public class TestableWithAttributeClass {}

    [Dependency(Environment = EnvironmentTestHelper.ExpectedEnvironment)]
    public class TestableWithAttributeAndEnvironmentClass {}

    public class TestableWithoutAttributeClass {}
}