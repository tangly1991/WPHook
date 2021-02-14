using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ame.WPHook
{
    public class HookAction : BaseAction
    {
        public HookAction(string tag, Action<object[]> action)
        {
            Callbacks = new Dictionary<int, Dictionary<string, Action<object[]>>>();

            string idx = tag + action.Method.Name;
            Callbacks.Add(
                Priority,
                new Dictionary<string, Action<object[]>>
                {
                    { idx, action }
                }
            );
        }

        public Dictionary<int, Dictionary<string, Action<object[]>>> Callbacks { get; }

        public override void AddAction(string tag, object action, int priority)
        {
            Action<object[]> _action = (Action<object[]>)action;
            string idx = tag + _action.Method.Name;

            if (!Callbacks.ContainsKey(priority))
            {
                Callbacks.Add(
                    priority,
                    new Dictionary<string, Action<object[]>>() {
                        { idx, _action }
                    }
                );
            }
            else
            {
                Callbacks[priority][idx] = _action;
            }
        }

        public override void RemoveAction(string tag, object action, int priority)
        {
            Action<object[]> _action = (Action<object[]>)action;
            string idx = tag + _action.Method.Name;
            if (Callbacks.ContainsKey(priority) && Callbacks[priority].ContainsKey(idx))
            {
                Callbacks[priority].Remove(idx);
            }
        }

        public override void DoAction(params object[] args)
        {
            if (Callbacks.Count() == 0)
            {
                return;
            }
            var iterations = Callbacks.Keys.ToArray();
            Array.Sort(iterations);
            foreach (int key in iterations)
            {
                var funcs = Callbacks[key];
                foreach (var the_ in funcs)
                {
                    the_.Value(args);
                }
            }
        }
    }
}
