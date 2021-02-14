using System;
using System.Collections.Generic;
using System.Text;

namespace Ame.WPHook
{
    public abstract class BaseAction
    {
        /// <summary>Gets or sets the type of the filter element.</summary>
        /// <value>The type of the filter element.</value>
        public Type ElementType { get; set; }

        /// <summary>Gets or sets a value indicating whether the filter is enabled by default.</summary>
        /// <value>true if the filter is enabled by default, false if not.</value>
        public int Priority { get; set; }

        public virtual void AddAction(string tag, object filter, int priority)
        {
            throw new Exception("AddAction");
        }

        public virtual void RemoveAction(string tag, object filter, int priority)
        {
            throw new Exception("RemoveAction");
        }

        public virtual void DoAction(params object[] args)
        {
            throw new Exception("DoAction");
        }
    }
}
