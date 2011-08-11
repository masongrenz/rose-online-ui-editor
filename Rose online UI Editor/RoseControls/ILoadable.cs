using System;
using System.Xml.XPath;

namespace Rose_online_UI_Editor.RoseControls
{
    interface ILoadable
    {
        void Load(XPathNavigator xmlNavigator);
    }
}
