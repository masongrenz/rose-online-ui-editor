using System;

using Rose_online_UI_Editor.Forms.CustomControls;

namespace Rose_online_UI_Editor.Command_Manager.TSICommand
{
    public class CmdAddDDS : ICommand
    {
        #region variables
        public ICustomControl Control { get; set; }
        private int DDSIndex;
        public string DDSname;
        #endregion

        #region methods
        public void Do()
        {
            DDSIndex = ((TSIDockContainer)Control).AddDDS(DDSname);
        }
        public void Undo()
        {
            ((TSIDockContainer)Control).RemoveDDS(DDSIndex);
        }
        public string GetInformation()
        {
            return "";
        }
        #endregion
    }
}