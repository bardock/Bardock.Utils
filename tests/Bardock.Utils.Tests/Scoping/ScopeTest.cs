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

            public _Inner Inner { get; set; }

            public Model(bool activated = false)
            {
                Activated = activated;
                Inner = new _Inner();
            }

            public class _Inner
            {
                public bool AutoDisable { get; set; }
            }
        }

        [Fact]
        public void Constructor_ValidArguments_ScopesValues()
        {
            var e = new Model();
            using (var scope = Scope.Create(e, b => b.Set(x => x.Activated, true)
                                                    .Set(x => x.Inner.AutoDisable, true)))
            {
                Assert.Equal(true, e.Activated);
                Assert.Equal(true, e.Inner.AutoDisable);
            }
            Assert.Equal(false, e.Activated);
            Assert.Equal(false, e.Inner.AutoDisable);
        }

        [Fact]
        public void Constructor_NullInstanceParam_ThrowsArgumentNullException()
        {
            Model e = null;
            var ex = Assert.Throws<ArgumentNullException>(() =>
            {
                using (var scope = Scope.Create(e, b => b.Set(x => x.Activated, true))) { }
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
            Assert.Equal("config", ex.ParamName);
        }
    }
}