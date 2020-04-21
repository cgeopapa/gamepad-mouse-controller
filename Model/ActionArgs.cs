namespace gamepad_mouse_controller.Model
{
    class ActionArgs
    {
        public bool down;
        public int x, y;
        public Gamepad gamepad;

        public bool L2, R2, L3, R3 = false;

        public ActionArgs(Gamepad gamepad)
        {
            this.gamepad = gamepad;
        }
    }
}
