namespace gamepad_mouse_controller.Model
{
    class ActionArgs
    {
        public bool up;
        public int x, y;
        public Gamepad gamepad;

        public ActionArgs(Gamepad gamepad)
        {
            this.gamepad = gamepad;
        }
    }
}
