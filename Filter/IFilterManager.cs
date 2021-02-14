using System;
using System.Collections.Generic;
using System.Text;

namespace Ame.WPHook
{
    public interface IFilterManager
    {
        public Dictionary<string, BaseFilter> GlobalFilters { get; }
        public void AddFilter<T>(string tag, Func<T, object[], T> queryFilter, int priority = 10);
        public void RemoveFilter<T>(string tag, Func<T, object[], T> queryFilter, int priority = 10);
        public T ApplyFilters<T>(string tag, T value, params object[] args);
    }
}
