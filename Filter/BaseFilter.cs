using System;
using System.Collections.Generic;
using System.Text;

namespace Ame.WPHook
{
    public abstract class BaseFilter
    {
        /// <summary>Gets or sets the type of the filter element.</summary>
        /// <value>The type of the filter element.</value>
        public Type ElementType { get; set; }

        /// <summary>Gets or sets a value indicating whether the filter is enabled by default.</summary>
        /// <value>true if the filter is enabled by default, false if not.</value>
        public int Priority { get; set; }

        public virtual void AddFilter(string tag, object filter, int priority)
        {
            throw new Exception("AddFilter");
        }

        public virtual void RemoveFilter(string tag, object filter, int priority)
        {
            throw new Exception("RemoveFilter");
        }

        /// <summary>Makes a deep copy of this filter.</summary>
        /// <param name="filterContext">The filter context that owns the filter copy.</param>
        /// <returns>A copy of this filter.</returns>
        //public virtual MovieBaseFilter Clone(AliasQueryFilterContext filterContext)
        //{
        //    throw new Exception("Clone");
        //}

        public virtual object ApplyFilters(object value, params object[] args)
        {
            throw new Exception("ApplyFilters");
        }
    }
}
