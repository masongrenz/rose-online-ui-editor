﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rose_online_UI_Editor.Files_Handlers.Common
{
    interface IReadable
    {
       void Load(string mypath, ClientType myclientType);
    }
}
