using Bardock.Utils.UnitTest.Data.EF.Effort.DataLoaders.Tests.DataLoaders;
using Bardock.Utils.UnitTest.Data.EF.Effort.DataLoaders.Tests.Entities;
using System.Linq;
using Xunit;

namespace Bardock.Utils.UnitTest.Data.EF.Effort.DataLoaders.Tests
{
    public class EntityObjectDataLoaderBindingsBuilderTests
    {
        [Fact]
        public void AddOfTEntity()
        {
            var builder = new EntityObjectDataLoaderBindingsBuilder();
            var dataLoader = new ModelDataLoader();

            builder.Add(dataLoader);

            var bindings = builder.GetBindings();

            Assert.True(dataLoader == bindings[typeof(Model).Name], "Bindings must contain a ModelDataLoader instance");
        }

        [Fact]
        public void Add()
        {
            var bindingName = "Model";
            var builder = new EntityObjectDataLoaderBindingsBuilder();
            var dataLoader = new ModelDataLoader();

            builder.Add(bindingName, dataLoader);

            var bindings = builder.GetBindings();

            Assert.True(dataLoader == bindings[bindingName], "Bindings must contain a ModelDataLoader instance");
        }

        [Fact]
        public void GetBindings()
        {
            var bindingName = "Model";
            var builder = new EntityObjectDataLoaderBindingsBuilder();
            var dataLoader = new ModelDataLoader();

            builder.Add(bindingName, dataLoader);

            var bindings = builder.GetBindings();

            Assert.True(bindings.Count() == 1, "Bindings count should be 1");
        }

        [Fact]
        public void GetBindings_Empty()
        {
            var builder = new EntityObjectDataLoaderBindingsBuilder();
            var dataLoader = new ModelDataLoader();

            var bindings = builder.GetBindings();

            Assert.True(!bindings.Any(), "Bindings count should be 0");
        }
    }
}