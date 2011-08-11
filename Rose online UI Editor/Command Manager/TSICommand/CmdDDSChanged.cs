using System;

using Rose_online_UI_Editor.Forms.CustomControls;
using Rose_online_UI_Editor.Files_Handlers;

namespace Rose_online_UI_Editor.Command_Manager.TSICommand
{
    class CmdDDSChanged : ICommand
    {
        #region variables
        public ICustomControl Control { get; set; }
        public int DDSIndex;        
        public TSI.DDS oldDDS;
        #endregion

        #region methods
        public void Do()
        {
            ((TSIDockContainer)Control).DDSChanged(DDSIndex);
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
