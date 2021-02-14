using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ame.WPHook
{
    public class HookFilter<T> : BaseFilter
    {
        public HookFilter(string tag, Func<T, object[], T> filter)
        {
            ElementType = typeof(T);
            Callbacks = new Dictionary<int, Dictionary<string, Func<T, object[], T>>>();

            string idx = tag + filter.Method.Name;
            Callbacks.Add(
                Priority,
                new Dictionary<string, Func<T, object[], T>>
                {
                    { idx, filter }
                }
            );
        }

        public Dictionary<int, Dictionary<string, Func<T, object[], T>>> Callbacks { get; }

        public override void AddFilter(string tag, object filter, int priority)
        {
            Func<T, object[], T> _filter = (Func<T, object[], T>)filter;
            string idx = tag + _filter.Method.Name;

            if (!Callbacks.ContainsKey(priority))
            {
                Callbacks.Add(
                    priority,
                    new Dictionary<string, Func<T, object[], T>>() {
                        { idx, _filter }
                    }
                );
            }
            else
            {
                Callbacks[priority][idx] = _filter;
            }
        }

        public override void RemoveFilter(string tag, object filter, int priority)
        {
            Func<T, object[], T> _filter = (Func<T, object[], T>)filter;
            string idx = tag + _filter.Method.Name;
            if (Callbacks.ContainsKey(priority) && Callbacks[priority].ContainsKey(idx))
            {
                Callbacks[priority].Remove(idx);
            }
        }

        public override object ApplyFilters(object value, params object[] args)
        {
            if (Callbacks.Count() == 0)
            {
                return value;
            }
            var iterations = Callbacks.Keys.ToArray();
            Array.Sort(iterations);
            foreach (int key in iterations)
            {
                var funcs = Callbacks[key];
                foreach (var the_ in funcs)
                {
                    value = the_.Value((T)value, args);
                }
            }
            return value;
        }
    }
}
