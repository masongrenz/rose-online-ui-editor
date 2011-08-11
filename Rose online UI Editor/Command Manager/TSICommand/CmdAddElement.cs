using System;

using Rose_online_UI_Editor.Forms.CustomControls;

namespace Rose_online_UI_Editor.Command_Manager.TSICommand
{
    class CmdAddElement : ICommand
    {
        #region variables
        public ICustomControl Control { get; set; }
        public int DDSIndex;
        public int elementIndex;
        public string elementName;
        #endregion

        #region methods
        public void Do()
        {
           ((TSIDockContainer)Control).AddSprite(elementName,DDSIndex,elementIndex);
        }
        public void Undo()
        {
            ((TSIDockContainer)Control).RemoveSprite(DDSIndex, elementIndex);
        }
        public string GetInformation()
        {
            return "";
        }
        #endregion
    }
}
