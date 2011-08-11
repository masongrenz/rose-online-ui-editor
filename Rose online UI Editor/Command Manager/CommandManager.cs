using System;
using System.Collections;


namespace Rose_online_UI_Editor.Command_Manager
{
    public class CommandManager
    {
        #region variables         
        private ArrayList commands;
        private const int COMMAND_MAX = 20;
        private int undoCount;
        #endregion
        
        #region constructors
        public CommandManager()
        {
            commands = new ArrayList(COMMAND_MAX);
            undoCount = 0;
        }
        #endregion

        #region methods
        public void executeCommand(ICommand command)
        {
            command.Do();
            if(commands.Count >= COMMAND_MAX -1)
            {
                commands.RemoveAt(0);
                commands.Add(command);
            }
            if(undoCount !=0)
            {
                commands.RemoveRange(commands.Count - undoCount, undoCount); //fixer ça !!! nasty
            }
            commands.Add(command);
        }

        public void undoCommand()
        {
            if ((commands.Count - 1) - undoCount >= 0)
            {
                ((ICommand)commands[(commands.Count - 1) - undoCount]).Undo();
                undoCount++;
            }            
        }
        public void recoCommand()
        {
            if (undoCount > 0)
            {
                undoCount--;
                ((ICommand)commands[(commands.Count - 1) - undoCount]).Do();                
            }
        }
        #endregion
    }
}
