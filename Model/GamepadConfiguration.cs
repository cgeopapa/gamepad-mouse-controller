using System.Collections.Generic;
using System.IO;
using gamepad_mouse_controller.Actions;
using Newtonsoft.Json;

namespace gamepad_mouse_controller.Model
{
    class GamepadConfiguration
    {
        private const string FILE_EXTENTION = ".gpconf";
        private readonly string name;
        public Dictionary<Buttons, IAction> config;

        public GamepadConfiguration(string name, int length)
        {
            this.name = name;
            try
            {
                Load();
            }
            catch(FileNotFoundException)
            {
                config = new Dictionary<Buttons, IAction>()
                {
                    { Buttons.A, new MouseLeftClickAction() },
                    { Buttons.B, new MouseRightClickAction() },
                    { Buttons.X, new WindowChangeAction() },
                    { Buttons.LeftShoulder, new BrowserBackAction() },
                    { Buttons.RightShoulder, new BrowserForwardAction() },
                    { Buttons.Back, new ShowSettingsAction() },
                    { Buttons.Start, new WindowsKeyAction() },
                    //{ Buttons.Up, new UpArrowAction() },
                    //{ Buttons.Right, new RightArrowAction() },
                    //{ Buttons.Down, new DownArrowAction() },
                    //{ Buttons.Left, new LeftArrowAction() },
                    { Buttons.LAxis, new MouseMoveAction() },
                    { Buttons.RAxis, new MouseScrollAction() },
                    //{ Buttons.L3_R3, new EnableDisableGamepadAction() },
                };
                //Save();
            }
        }

        public void Save()
        {
            StreamWriter file = new StreamWriter(name + FILE_EXTENTION);
            file.Write(JsonConvert.SerializeObject(config));
            file.Flush();
            file.Close();
        }

        public void Load()
        {
            StreamReader file = new StreamReader(name + FILE_EXTENTION);
            var fileDictionary = JsonConvert.DeserializeObject<Dictionary<Buttons, string>>(file.ReadToEnd());
            var actionMap = ActionMap.actionMap;
            config.Clear();
            foreach(var i in fileDictionary)
            {
                config.Add(i.Key, actionMap[i.Value]);
            }
            file.Close();
        }
    }
}
