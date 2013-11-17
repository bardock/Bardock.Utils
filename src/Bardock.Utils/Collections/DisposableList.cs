using System;
using System.Collections.Generic;

namespace Bardock.Utils.Collections
{
    /// <summary>
    /// Represents a strongly typed list of disposable objects.
    /// </summary>
	public class DisposableList<T> : List<T>, IDisposable where T : IDisposable
	{
        public void Dispose()
        {
            foreach (var x in this)
            {
                if ((x != null))
                {
                    x.Dispose();
                }
            }
        }
	}

}