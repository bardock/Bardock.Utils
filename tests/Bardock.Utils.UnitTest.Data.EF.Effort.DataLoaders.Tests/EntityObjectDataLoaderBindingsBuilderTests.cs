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
            var builder = new EntityObjectDataLoader.BindingsBuilder();

            builder.Add<ModelDataLoader>();

            var bindings = builder.Build();

            Assert.True(bindings.ContainsKey("Model"), "Bindings must contain a ModelDataLoader instance");
        }

        [Fact]
        public void GetBindings()
        {
            var builder = new EntityObjectDataLoader.BindingsBuilder();

            builder.Add<ModelDataLoader>();

            var bindings = builder.Build();

            Assert.True(bindings.Count() == 1, "Bindings count should be 1");
        }

        [Fact]
        public void GetBindings_Empty()
        {
            var builder = new EntityObjectDataLoader.BindingsBuilder();
            var dataLoader = new ModelDataLoader();

            var bindings = builder.Build();

            Assert.True(!bindings.Any(), "Bindings count should be 0");
        }
    }
}