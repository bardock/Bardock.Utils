using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bardock.Utils.Sync;
using Xunit;

namespace Bardock.Utils.Tests.Types
{
    public class StringLockerTest
    {
        private const int NUM_TASKS = 20000;
        private static StringLocker _stringLocker = new StringLocker();

        [Fact]
        public void LockUsingSameStringButDifferentInstance_RaceCondition()
        {
            var c = 0;
            Action incrementer = () =>
            {
                lock (GetKey())
                {
                    c++;
                }
            };

            Task.WaitAll(StartTasks(NUM_TASKS, incrementer).ToArray());

            Assert.True(NUM_TASKS > c, "Race condition is expected, so the final value of 'c' must be lower than the number of tasks that increment it");
        }

        [Fact]
        public void LockUsingStringLockerObjectPassingSameStringButDifferentInstance_Synced()
        {
            var c = 0;
            Action incrementer = () =>
            {
                lock (_stringLocker.GetLockObject(GetKey()))
                {
                    c++;
                }
            };

            Task.WaitAll(StartTasks(NUM_TASKS, incrementer).ToArray());

            Assert.Equal(NUM_TASKS, c);
        }

        private string GetKey()
        {
            return new String("key".ToCharArray());
        }

        private IEnumerable<Task> StartTasks(int q, Action action)
        {
            for (var i = 0; i < q; i++)
            {
                yield return Task.Factory.StartNew(action);
            }
        }
    }
}