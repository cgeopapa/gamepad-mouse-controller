using System.Collections.Generic;
using System.IO;
using gamepad_mouse_controller.Actions;
using gamepad_mouse_controller.Model.Buttons;
using Newtonsoft.Json;
using System.Linq;
using System;

namespace gamepad_mouse_controller.Model
{
    class GamepadConfiguration
    {
        private const string FILE_EXTENTION = ".gpconf";
        private readonly string name;
        private Dictionary<GamepadButtons, string> config;

        public IAction[] action;

        public GamepadConfiguration(string name, int length)
        {
            this.name = name;
            var actionMap = ActionMap.GetActionMap();

            try
            {
                Load();
            }
            catch(FileNotFoundException)
            {
                config = new Dictionary<GamepadButtons, string>()
                {
                    { GamepadButtons.X, typeof(MouseLeftClickAction).Name },
                    { GamepadButtons.O, typeof(MouseRightClickAction).Name },
                    { GamepadButtons.Square, typeof(WindowChangeAction).Name },
                    { GamepadButtons.L1, typeof(BrowserBackAction).Name },
                    { GamepadButtons.R1, typeof(BrowserForwardAction).Name },
                    { GamepadButtons.Select, typeof(ShowSettingsAction).Name },
                    { GamepadButtons.Start, typeof(WindowsKeyAction).Name },
                    { GamepadButtons.Up, typeof(UpArrowAction).Name },
                    { GamepadButtons.Right, typeof(RightArrowAction).Name },
                    { GamepadButtons.Down, typeof(DownArrowAction).Name },
                    { GamepadButtons.Left, typeof(LeftArrowAction).Name },
                    { GamepadButtons.LAxis, typeof(MouseMoveAction).Name },
                    { GamepadButtons.RAxis, typeof(MouseScrollAction).Name },
                    { GamepadButtons.L3_R3, typeof(EnableDisableGamepadAction).Name },
                };
                //Save();
            }

            int max = (int)(Enum.GetValues(typeof(GamepadButtons)).Cast<GamepadButtons>().Max() + 1);
            action = new IAction[max];
            foreach (GamepadButtons button in (GamepadButtons[])Enum.GetValues(typeof(GamepadButtons)))
            {
                if(config.ContainsKey(button))
                {
                    action[(int)button] = actionMap[config[button]];
                }
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
            config = JsonConvert.DeserializeObject<Dictionary<GamepadButtons, string>>(file.ReadToEnd());
            file.Close();
        }
    }
}
