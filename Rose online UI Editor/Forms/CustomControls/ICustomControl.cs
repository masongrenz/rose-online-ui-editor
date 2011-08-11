using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rose_online_UI_Editor.Forms.CustomControls
{
    public interface ICustomControl
    {
        void Load(string path);
        void Reload();
        void Save();

    }
}
