using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace gamepad_mouse_controller.Model
{
    class ActionMap
    {
        public static readonly Dictionary<string, IAction> actionMap = new Dictionary<string, IAction>();

        public ActionMap()
        {
            var types = Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .Where(t => t.Namespace.Equals("Actions"));

            foreach (var t in types)
            {
                IAction action = (IAction)Activator.CreateInstance(t);
                actionMap.Add(action.Name, action);
            }
        }
    }
}
