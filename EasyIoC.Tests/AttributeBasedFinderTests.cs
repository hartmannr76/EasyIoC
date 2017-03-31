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
    [TestFixture]
    public class AttributeBasedFinderTests
    {
        private AttributeBasedFinder _service;
        private List<Assembly> _mockAssemblies;

        [SetUp]
        public void Setup()
        {
            _service = new AttributeBasedFinder();
            _mockAssemblies = null;
        }

        [Test]
        public void FindRegisteredClasses_GivenClassWithAttribute_ReturnsType()
        {
            GivenAssembliesHasType(typeof(TestableWithAttributeClass));

            var result = _service.FindRegisteredClasses(_mockAssemblies);

            Assert.AreEqual(1, result.Count());
            Assert.AreEqual(typeof(TestableWithAttributeClass), result.First());
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
                .Returns(() => new[] {type.GetTypeInfo()});

            assemblyList.Add(mockAssembly.Object);

            _mockAssemblies = assemblyList;
        }
    }

    [Dependency(Environment = Environments.Development)]
    public class TestableWithAttributeClassAndOneEnvironment {}

    [Dependency(Environment = Environments.Production)]
    public class TestableWithAttributeClassAndDifferentEnvironment {}

    [Dependency]
    public class TestableWithAttributeClass {}

    public class TestableWithoutAttributeClass {}
}