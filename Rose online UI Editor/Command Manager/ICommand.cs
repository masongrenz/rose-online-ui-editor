using System;

using Rose_online_UI_Editor.Forms.CustomControls;

namespace Rose_online_UI_Editor.Command_Manager
{
   public interface ICommand
    {
        /// <summary>
        /// Set the Custom Control   
        /// </summary>
        ICustomControl Control {get; set;}

        /// <summary>
        /// Execute the command 
        /// </summary>
        void Do();

        /// <summary>
        ///  Come back to the initial state 
        /// </summary>
        void Undo();

       /// <summary>
       /// Get Information of what this command did  
       /// </summary>
       string GetInformation();
    }
}
