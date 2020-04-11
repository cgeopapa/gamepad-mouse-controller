using System.Collections.Generic;

namespace gamepad_mouse_controller.Actions
{
    class ActionMap
    {
        private readonly Dictionary<string, IAction> actionMap = new Dictionary<string, IAction>();

        private static ActionMap instance = null;

        private ActionMap()
        {
            IAction action = new MouseLeftClickAction();
            actionMap.Add(action.Name, action);

            action = new MouseRightClickAction();
            actionMap.Add(action.Name, action);

            action = new MouseMoveAction();
            actionMap.Add(action.Name, action);

            action = new WindowsKeyAction();
            actionMap.Add(action.Name, action);

            action = new BrowserBackAction();
            actionMap.Add(action.Name, action);
            
            action = new BrowserForwardAction();
            actionMap.Add(action.Name, action);
            
            action = new MouseScrollAction();
            actionMap.Add(action.Name, action);
        }

        public static Dictionary<string, IAction> GetActionMap()
        {
            if (instance == null)
            {
                instance = new ActionMap();
            }
            return instance.actionMap;
        }
    }
}
