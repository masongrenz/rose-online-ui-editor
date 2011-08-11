using System;

using Rose_online_UI_Editor.Forms.CustomControls;
using Rose_online_UI_Editor.Files_Handlers;

namespace Rose_online_UI_Editor.Command_Manager.TSICommand
{
    class CmdPasteElement : ICommand
    {
        #region variables
        public ICustomControl Control { get; set; }
        public TSI.DDS.DDSElement newElement;
        public int DDSIndex;
        public int elementIndex;

        private TSI.DDS.DDSElement oldElement;
        #endregion

        #region methods
        public void Do()
        {
            oldElement = ((TSIDockContainer)Control).GetSprite(DDSIndex, elementIndex);
            ((TSIDockContainer)Control).SetSprite(newElement, DDSIndex, elementIndex);
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
