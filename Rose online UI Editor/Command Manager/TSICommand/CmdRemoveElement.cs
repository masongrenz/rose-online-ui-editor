using System;

using Rose_online_UI_Editor.Forms.CustomControls;
using Rose_online_UI_Editor.Files_Handlers;

namespace Rose_online_UI_Editor.Command_Manager.TSICommand
{
    class CmdRemoveElement : ICommand
    {
        #region variables
        public ICustomControl Control { get; set; }
        private TSI.DDS.DDSElement oldElement;
        public int DDSIndex;
        public int elementIndex;        
        #endregion

        #region methods
        public void Do()
        {
            oldElement = ((TSIDockContainer)Control).RemoveSprite(DDSIndex, elementIndex);
        }
        public void Undo()
        {
            ((TSIDockContainer)Control).AddSprite(oldElement,DDSIndex, elementIndex);
        }
        public string GetInformation()
        {
            return "";
        }
        #endregion
    }
}
