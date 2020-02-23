namespace gamepad_mouse_controller.Controller
{
    interface IInput
    {
        void OnXDown();
        void OnXUp();
        void OnSDown();
        void OnSUp();
        void OnTDown();
        void OnTUp();
        void OnODown();
        void OnOUp();
        void OnR1Down();
        void OnR1Up();
        void OnL1Down();
        void OnL1Up();
        void OnL3Down();
        void OnL3Up();
        void OnR3Down();
        void OnR3Up();
        void OnStartDown();
        void OnStartUp();
        void OnSelectDown();
        void OnSelectUp();
        
        void OnUpDown();
        void OnUpUp();
        void OnRightDown();
        void OnRightUp();
        void OnDownDown();
        void OnDownUp();
        void OnLeftDown();
        void OnLeftUp();

        void LAxis(int axis);
        void RAxis(int axis);
        void R2L2Axis(int axis);
    }
}
