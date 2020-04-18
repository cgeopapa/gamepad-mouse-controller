namespace gamepad_mouse_controller.Model
{
    class ActionArgs
    {
        public bool down;
        public int x, y;
        public Gamepad gamepad;

        public ActionArgs(Gamepad gamepad)
        {
            this.gamepad = gamepad;
        }
    }
}
