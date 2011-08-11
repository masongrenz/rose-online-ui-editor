using System;

namespace Rose_online_UI_Editor.Command_Manager
{
    interface IUseCommand
    {
        /// <summary>
        ///  Interface to get the Command Manager of a control
        /// </summary>
        CommandManager getManager();
    }
}
