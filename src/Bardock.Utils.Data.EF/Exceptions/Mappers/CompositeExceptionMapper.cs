using System;
using System.Collections.Generic;
using System.Linq;

namespace Bardock.Utils.Data.EF.Exceptions.Mappers
{
    public class CompositeExceptionMapper : IExceptionMapper
    {
        private List<IExceptionMapper> _children;

        public CompositeExceptionMapper(params IExceptionMapper[] mappers)
        {
            _children = mappers.ToList();
        }

        public void Add(IExceptionMapper ex)
        {
            _children.Add(ex);
        }

        public virtual Exception Map(Exception ex)
        {
            foreach (IExceptionMapper m in _children)
            {
                var retEx = m.Map(ex);
                if (retEx != ex)
                    return retEx;
            }
            return ex;
        }
    }
}