using System;
using System.Collections.Generic;
using Xunit;
using Bardock.Utils.Extensions;
using Bardock.Utils.Collections;

namespace Bardock.Utils.Tests.Extensions
{
    public class IDictionaryExtensionsTest
    {
        #region "Test Helpers"

        private void AssertContainItem<TKey,TValue>(IDictionary<TKey, TValue> sut, TKey key, TValue value)
        {
            Assert.Contains(key, sut.Keys);
            Assert.Contains(value, sut.Values);
        }

        private void AssertDoesNotContainItem<TKey,TValue>(IDictionary<TKey, TValue> sut, TKey key, TValue value)
        {
            Assert.DoesNotContain(key, sut.Keys);
            Assert.DoesNotContain(value, sut.Values);
        }

        #endregion

        [Fact]
        public void AddItem_NullKey_ShouldThrowArgumentNullException()
        {
            // Setup
            var sut = new Dictionary<string,string>();

            // Exercise && Verify
            var ex = Assert.Throws<ArgumentNullException>(() => 
                sut.AddItem(null, "value"));

            Assert.Equal("key", ex.ParamName);
        }

        [Fact]
        public void AddItem_NullValue_ShouldContainItem()
        {
            // Setup
            var sut = new Dictionary<string, string>();
            var expectedKey = "key";
            var expectedValue = (string)null;

            // Exercise 
            sut.AddItem(expectedKey, expectedValue);

            // Verify
            AssertContainItem(sut, expectedKey, expectedValue);
        }

        [Fact]
        public void AddItem_NotNullValue_ShouldContainItem()
        {
            // Setup
            var sut = new Dictionary<string, string>();
            var expectedKey = "key";
            var expectedValue = "value";

            // Exercise 
            sut.AddItem(expectedKey, expectedValue);

            // Verify
            AssertContainItem(sut, expectedKey, expectedValue);
        }

        [Fact]
        public void AddItemWhen_WhenFalse_ShouldNotContainItem()
        {
            // Setup
            var sut = new Dictionary<string, string>();
            var expectedKey = "key";
            var expectedValue = "value";

            // Exercise 
            sut.AddItem(false, expectedKey, expectedValue);

            // Verify
            AssertDoesNotContainItem(sut, expectedKey, expectedValue);
        }

        [Fact]
        public void AddItemWhen_WhenFalseAndNullKey_ShouldThrowArgumentNullException()
        {
            // Setup
            var sut = new Dictionary<string, string>();

            // Exercise && Verify
            var ex = Assert.Throws<ArgumentNullException>(() => 
                sut.AddItem(false, null, "value"));

            Assert.Equal("key", ex.ParamName);
        }

        [Fact]
        public void AddItemWhen_WhenTrue_ShouldContainItem()
        {
            // Setup
            var sut = new Dictionary<string, string>();
            var expectedKey = "key";
            var expectedValue = "value";

            // Exercise 
            sut.AddItem(true, expectedKey, expectedValue);

            // Verify
            AssertContainItem(sut, expectedKey, expectedValue);
        }

        [Fact]
        public void AddItemWhen_WhenTrueAndNullKey_ShouldThrowArgumentNullException()
        {
            // Setup
            var sut = new Dictionary<string, string>();

            // Exercise && Verify
            var ex = Assert.Throws<ArgumentNullException>(() =>
                sut.AddItem(true, null, "value"));

            Assert.Equal("key", ex.ParamName);
        }

        [Fact]
        public void Merge_SingleDiccionary_ShouldReturnMergedDiccionary()
        {
            // Setup
            var sut = new Dictionary<string, string>() {
                {"key","value"}
            }.AsIDictionary();

            var arg = new Dictionary<string, string>() {
                {"otherkey","testvalue"}
            }.AsIDictionary();

            // Exercise
            var actual = sut.Merge(arg);

            // Verify
            Assert.NotSame(sut, actual);
            Assert.Equal(actual["key"], "value");
            Assert.Equal(actual["otherkey"], "testvalue");
        }

        [Fact]
        public void Merge_SingleDiccionaryOverrideKey_ShouldReturnMergedDiccionaryWithKeyOverriden()
        {
            // Setup
            var sut = new Dictionary<string, string>() {
                {"key","value"}
            }.AsIDictionary();

            var arg = new Dictionary<string, string>() {
                {"key","value2"},
                {"otherkey","testvalue"}
            }.AsIDictionary();

            // Exercise
            var actual = sut.Merge(arg);

            // Verify
            Assert.NotSame(sut, actual);
            Assert.Equal(actual["key"], "value2");
            Assert.Equal(actual["otherkey"], "testvalue");
        }
        
        [Fact]
        public void Merge_EmptyDicctionary_ShouldReturnMergedDiccionary()
        {
            // Setup
            var sut = new Dictionary<string, string>() {
                {"key","value"}
            }.AsIDictionary();

            var arg = new Dictionary<string, string>().AsIDictionary();

            // Exercise
            var actual = sut.Merge(arg);

            // Verify
            Assert.NotSame(sut, actual);
            Assert.Equal(actual["key"], "value");
        }
        
        [Fact]
        public void Merge_NullDicctionary_ShouldThrowArgumentNullException()
        {
            // Setup
            var sut = new Dictionary<string, string>() {
                {"key","value"}
            }.AsIDictionary();

            var arg = (IDictionary<string, string>)null;

            // Exercise & Verify
            var ex = Assert.Throws<ArgumentNullException>(() =>
               sut.Merge(arg));

            Assert.Equal("arg", ex.ParamName);
        }
        
        [Fact]
        public void Merge_NullArrayDicctionary_ShouldThrowArgumentNullException()
        {
            // Setup
            var sut = new Dictionary<string, string>() {
                {"key","value"}
            }.AsIDictionary();

            var arg = (IDictionary<string, string>[])null;

            // Exercise & Verify
            var ex = Assert.Throws<ArgumentNullException>(() =>
                sut.Merge(arg));

            Assert.Equal("args", ex.ParamName);
        }
        
        [Fact]
        public void Merge_ArrayDicctionaryWithNullElement_ShouldThrowArgumentException()
        {
            // Setup
            var sut = new Dictionary<string, string>() {
                {"key","value"}
            }.AsIDictionary();

            var arg = new IDictionary<string, string>[]{
                new Dictionary<string, string>() {
                    {"key1","value1"},
                    {"key2","value2"},
                    {"key3","value3"}
                },
                new Dictionary<string, string>() {
                    {"key4","value4"},
                    {"key5","value5"}
                },
                null
            };

            // Exercise & Verify
            var ex = Assert.Throws<ArgumentException>(() =>
                sut.Merge(arg));

            Assert.Equal("args", ex.ParamName);
        }
        
        [Fact]
        public void Merge_ArrayDicctionaryWithNullValue_ShouldContainItem()
        {
            // Setup
            var sut = new Dictionary<string, string>() {
                {"key","value"}
            }.AsIDictionary();
            
            var expectedKey = "key6";
            var expectedValue = (string)null;

            IDictionary<string, string>[] arg = {
                new Dictionary<string, string>() {
                    {"key1","value1"},
                    {"key2","value2"},
                    {"key3","value3"}
                }.AsIDictionary(),
                new Dictionary<string, string>() {
                    {"key4","value4"},
                    {"key5","value5"}
                }.AsIDictionary(),
                new Dictionary<string, string>() {
                    {expectedKey,expectedValue}
                }.AsIDictionary()
            };

            // Exercise
            
            var actual = sut.Merge(arg);
            //Verify
            AssertContainItem(actual, expectedKey, expectedValue);
        }

        [Fact]
        public void Merge_ArrayDicctionary_ShouldReturnMergedDiccionary()
        {
            // Setup
            var sut = new Dictionary<string, string>() {
                {"key","value"}
            }.AsIDictionary();

            IDictionary<string, string>[] arg  = {
                new Dictionary<string, string>() {
                    {"key1","value1"},
                    {"key2","value2"},
                    {"key3","value3"}
                }.AsIDictionary(),
                new Dictionary<string, string>() {
                    {"key4","value4"},
                    {"key5","value5"}
                }.AsIDictionary()
            };

            // Exercise
            var actual = sut.Merge(arg);

            // Verify
            Assert.NotSame(sut, actual);
            Assert.Equal(actual["key"], "value");
            Assert.Equal(actual["key2"], "value2");
            Assert.Equal(actual["key3"], "value3");
        }
        
        [Fact]
        public void Merge_ArrayDicctionaryOverrideKey_ShouldReturnMergedDiccionaryWithKeyOverriden()
        {
            // Setup
            var sut = new Dictionary<string, string>() {
                {"key","value"}
            }.AsIDictionary();

            IDictionary<string, string>[] arg = {
                new Dictionary<string, string>() {
                    {"key","new value"},
                    {"key2","value2"},
                    {"key3","value3"}
                }.AsIDictionary(),
                new Dictionary<string, string>() {
                    {"key4","value4"},
                    {"key2","new value2"}
                }.AsIDictionary()
            };

            // Exercise
            var actual = sut.Merge(arg);

            // Verify
            Assert.NotSame(sut, actual);
            Assert.Equal(actual["key"], "new value");
            Assert.Equal(actual["key2"], "new value2");
        }
    }

    public static class DictionaryExtensions
    {
        public static IDictionary<TKey, TValue> AsIDictionary<TKey, TValue>(
            this IDictionary<TKey, TValue> @this)
        {
            return (IDictionary<TKey, TValue>)@this;
        }
    }
}
