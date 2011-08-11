#region using
using System;
#endregion

namespace Rose_online_UI_Editor
{
    #region delegate
    public delegate void DelegateAppendText(string text);
    #endregion

    #region class StatusManager
   public class StatusManager
    {
        #region static variable      
        public static DelegateAppendText fctAppendText;
        #endregion

        #region constructor
        public StatusManager()
        {

        }
        #endregion

        #region static methods
        public static void RegisterDelegate(DelegateAppendText fct)
        {
            fctAppendText = fct;
        }

        public static void AddLog(string message)
        {
            fctAppendText("[" + System.DateTime.Now + "] : " + message);
            fctAppendText("\n");
        }    
        
        #endregion
    }
    #endregion
}
