using System;

using Rose_online_UI_Editor.Forms.CustomControls;
using Rose_online_UI_Editor.Files_Handlers;

namespace Rose_online_UI_Editor.Command_Manager.TSICommand
{
    class CmdElementChanged : ICommand
    {
        #region variables
        public ICustomControl Control { get; set; }
        public int DDSIndex;
        public int elementIndex;
        public TSI.DDS.DDSElement oldElement;
        #endregion

        #region methods
        public void Do()
        {
            ((TSIDockContainer)Control).ElementChanged(DDSIndex, elementIndex);
        }
        public void Undo()
        {
            ((TSIDockContainer)Control).SetSprite(oldElement, DDSIndex, elementIndex);
        }
        public string GetInformation()
        {
            return "";
        }
        #endregion
    }
}
