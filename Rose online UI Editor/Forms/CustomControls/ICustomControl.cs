using System;

using Rose_online_UI_Editor.Mouse;

namespace Rose_online_UI_Editor.Forms.CustomControls
{
    public interface ICustomControl
    {
        void SetMouseType(MouseType mouseType);
        void Load(string path);
        void Reload();
        void Save();

    }
}
