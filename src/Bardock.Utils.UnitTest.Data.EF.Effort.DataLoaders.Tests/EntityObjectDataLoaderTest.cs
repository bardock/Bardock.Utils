using Bardock.Utils.UnitTest.Data.EF.Effort.DataLoaders.Tests.DataLoaders;
using Bardock.Utils.UnitTest.Data.EF.Effort.DataLoaders.Tests.Entities;
using Bardock.Utils.UnitTest.Data.EF.Effort.DataLoaders.Tests.Extensions;
using Bardock.Utils.UnitTest.Data.EF.Effort.DataLoaders.Tests.Helpers;
using Effort.DataLoaders;
using System;
using System.Linq;
using Xunit;

namespace Bardock.Utils.UnitTest.Data.EF.Effort.DataLoaders.Tests
{
    public class EntityObjectDataLoaderTest
    {
        [Fact]
        public void Ctor_Builder_Null()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                return new EntityObjectDataLoader(null);
            });
        }

        [Fact]
        public void Ctor_Builder_Empty()
        {
            var bindings = new EntityObjectDataLoaderBindingsBuilder();

            var loader = new EntityObjectDataLoader(bindings);

            Assert.Throws<EntityObjectDataLoader.NotValidBindingsException>(() =>
            {
                loader.CreateTableDataLoaderFactory();
            });
        }

        [Fact]
        public void CreateTableDataLoaderFactory()
        {
            var bindings = new EntityObjectDataLoaderBindingsBuilder();

            var modelLoader = new ModelDataLoader();

            bindings.Add(modelLoader);

            var loader = new EntityObjectDataLoader(bindings);

            var factory = loader.CreateTableDataLoaderFactory();

            var dataLoader = factory.CreateTableDataLoader(BuildTableDescription());

            var entityLoader = ((EntityObjectDataLoaderWrapper)dataLoader).GetEntityDataLoader();

            Assert.True(modelLoader == entityLoader);
        }

        [Fact]
        public void GetData()
        {
            var bindings = new EntityObjectDataLoaderBindingsBuilder();

            var modelLoader = new ModelDataLoader();

            bindings.Add(modelLoader);

            var loader = new EntityObjectDataLoader(bindings);

            var factory = loader.CreateTableDataLoaderFactory();

            var dataLoader = factory.CreateTableDataLoader(BuildTableDescription());

            var data = dataLoader.GetData().First();

            Assert.Equal(1, data[0]);
            Assert.Equal("Test", data[1]);
            Assert.Equal("TST", data[2]);
        }

        private TableDescription BuildTableDescription()
        {
            var builder = new TableDescriptionBuilder<Model>();

            return builder
                .Add(x => x.Id)
                .Add(x => x.Name)
                .Add(x => x.Description)
                .Build();
        }
    }
}