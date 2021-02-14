using System;
using System.Collections.Generic;
using System.Text;

namespace Ame.WPHook
{
    public class ActionManager : IActionManager
    {
        public ActionManager()
        {
            GlobalActions = new Dictionary<string, BaseAction>();
        }

        public Dictionary<string, BaseAction> GlobalActions { get; }

        /// <summary>
        ///     Creates a action associated with the specified key added for the context.
        /// </summary>
        /// <param name="key">The action key associated to the action.</param>
        /// <param name="queryAction">The query action to apply to the context.</param>
        /// <param name="priority">true if the action is priority.</param>
        public void AddAction(string tag, Action<object[]> queryAction, int priority = 10)
        {
            if (!GlobalActions.ContainsKey(tag))
            {
                GlobalActions.Add(
                    tag,
                    new HookAction(tag, queryAction) { Priority = priority }
                );
            }
            else
            {
                GlobalActions[tag].AddAction(tag, queryAction, priority);
            }
        }
        public void RemoveFilter(string tag, Action<object[]> queryFilter, int priority = 10)
        {
            if (GlobalActions.ContainsKey(tag))
            {
                GlobalActions[tag].RemoveAction(tag, queryFilter, priority);
            }
        }

        public void DoAction(string tag, params object[] args)
        {
            if (GlobalActions.TryGetValue(tag, out BaseAction action))
            {
                action.DoAction(args);
            }
        }
    }
}
