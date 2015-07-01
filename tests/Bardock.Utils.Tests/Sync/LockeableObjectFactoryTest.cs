using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bardock.Utils.Sync;
using Xunit;

namespace Bardock.Utils.Tests.Sync
{
    public class LockeableObjectFactoryTest
    {
        private const int NUM_TASKS = 20000;
        private static LockeableObjectFactory<string> _lockeableStringFactory = new LockeableObjectFactory<string>();

        /// <summary>
        /// This test only demostrates that not using LockeableObjectFactory produces race condition.
        /// The same process is used later to verify that LockeableObjectFactory solves this problem.
        /// </summary>
        [Fact]
        public void LockUsingSameStringButDifferentInstance_RaceCondition()
        {
            var c = 0;
            Action incrementer = () =>
            {
                lock (GenerateKey())
                {
                    c++;
                }
            };

            Task.WaitAll(StartTasks(NUM_TASKS, incrementer).ToArray());

            Assert.True(c < NUM_TASKS, "Race condition is expected, so the final value of 'c' must be lower than the number of tasks that increment it");
        }

        [Fact]
        public void LockUsingLockeableObjectFactoryPassingSameStringButDifferentInstance_Synced()
        {
            var c = 0;
            Action incrementer = () =>
            {
                lock (_lockeableStringFactory.Get(GenerateKey()))
                {
                    c++;
                }
            };

            Task.WaitAll(StartTasks(NUM_TASKS, incrementer).ToArray());

            Assert.Equal(NUM_TASKS, c);
        }

        private string GenerateKey()
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