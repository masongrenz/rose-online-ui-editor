using System;

using Rose_online_UI_Editor.Forms.CustomControls;
using Rose_online_UI_Editor.Files_Handlers;

namespace Rose_online_UI_Editor.Command_Manager.TSICommand
{
    public class CmdRemoveDDS : ICommand
    {
        #region variables 
        public ICustomControl Control { get; set; }
        public int DDSIndex;
        private TSI.DDS oldDDS;
        #endregion

        #region methods
        public void Do()
        {
            oldDDS = ((TSIDockContainer)Control).RemoveDDS(DDSIndex);
        }
        public void Undo()
        {
            ((TSIDockContainer)Control).AddDDS(oldDDS,DDSIndex);
        }
        public string GetInformation()
        {
            return "";
        }
        #endregion
    }
}
