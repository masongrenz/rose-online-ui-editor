using System;

using Rose_online_UI_Editor.Forms.CustomControls;
using Rose_online_UI_Editor.Files_Handlers;

namespace Rose_online_UI_Editor.Command_Manager.TSICommand
{
    class CmdPasteDDS : ICommand
    {
        #region variables
        public ICustomControl Control { get; set; }
        public TSI.DDS newDDS;
        public int DDSIndex;       

        private TSI.DDS oldDDS;
        #endregion

        #region methods
        public void Do()
        {
            oldDDS = ((TSIDockContainer)Control).GetDDS(DDSIndex);
            ((TSIDockContainer)Control).SetDDS(newDDS, DDSIndex);
        }
        public void Undo()
        {
            ((TSIDockContainer)Control).SetDDS(oldDDS, DDSIndex);
        }
        public string GetInformation()
        {
            return "";
        }
        #endregion
    }
}
