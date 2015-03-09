using Bardock.Utils.Collections;
using Bardock.Utils.Scoping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Bardock.Utils.Tests.Scoping
{
    public class ScopeTest
    {
        public class Model
        {
            public bool Activated { get; set; }

            public Model(bool activated = false)
            {
                Activated = activated;
            }
        }

        [Fact]
        public void Constructor_ValidArguments_ScopesValues()
        {
            var e = new Model();
            using (var scope = Scope.Create(e, b => b.Add(x => x.Activated, true)))
            {
                Assert.Equal(true, e.Activated);
            }
            Assert.Equal(false, e.Activated);
        }

        [Fact]
        public void Constructor_NullInstanceParam_ThrowsArgumentNullException()
        {
            Model e = null;
            var ex = Assert.Throws<ArgumentNullException>(() =>
            {
                using (var scope = Scope.Create(e, b => b.Add(x => x.Activated, true))) { }
            });
            Assert.Equal("instance", ex.ParamName);
        }

        [Fact]
        public void Constructor_NullFactoryFuncParam_ThrowsArgumentNullException()
        {
            var e = new Model();
            var ex = Assert.Throws<ArgumentNullException>(() =>
            {
                using (var scope = Scope.Create(e, null)) { }
            });
            Assert.Equal("factoryFunc", ex.ParamName);
        }
    }
}