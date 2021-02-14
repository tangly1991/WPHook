using System;
using System.Collections.Generic;
using System.Text;

namespace Ame.WPHook
{
    public class FilterManager : IFilterManager
    {
        public FilterManager()
        {
            GlobalFilters = new Dictionary<string, BaseFilter>();
        }

        public Dictionary<string, BaseFilter> GlobalFilters { get; }

        /// <summary>
        ///     Creates a filter associated with the specified key added for the context.
        /// </summary>
        /// <typeparam name="T">The type of elements of the query.</typeparam>
        /// <param name="key">The filter key associated to the filter.</param>
        /// <param name="queryFilter">The query filter to apply to the context.</param>
        /// <param name="priority">true if the filter is priority.</param>
        public void AddFilter<T>(string tag, Func<T, object[], T> queryFilter, int priority = 10)
        {
            if (!GlobalFilters.ContainsKey(tag))
            {
                GlobalFilters.Add(
                    tag,
                    new HookFilter<T>(tag, queryFilter) { Priority = priority }
                );
            }
            else
            {
                GlobalFilters[tag].AddFilter(tag, queryFilter, priority);
            }
        }

        public void RemoveFilter<T>(string tag, Func<T, object[], T> queryFilter, int priority = 10)
        {
            if (GlobalFilters.ContainsKey(tag))
            {
                GlobalFilters[tag].RemoveFilter(tag, queryFilter, priority);
            }
        }

        public T ApplyFilters<T>(string tag, T value, params object[] args)
        {
            if (!GlobalFilters.TryGetValue(tag, out BaseFilter filter))
            {
                return value;
            }
            else
            {
                return (T)filter.ApplyFilters(value, args);
            }
        }
    }
}
