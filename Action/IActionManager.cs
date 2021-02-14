using System;
using System.Collections.Generic;
using System.Text;

namespace Ame.WPHook
{
    public interface IActionManager
    {
        public Dictionary<string, BaseAction> GlobalActions { get; }
        public void AddAction(string tag, Action<object[]> queryAction, int priority = 10);
        public void RemoveFilter(string tag, Action<object[]> queryFilter, int priority = 10);
        public void DoAction(string tag, params object[] args);
    }
}
