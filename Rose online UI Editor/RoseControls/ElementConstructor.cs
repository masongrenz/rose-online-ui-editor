using System;
using System.Xml.XPath;

namespace Rose_online_UI_Editor.RoseControls
{
    public class ElementConstructor
    {
        public static Caption CreateElement(XPathNavigator xmlNavigator)
        {         
            string elmtName = xmlNavigator.Name;
            if (elmtName == "CAPTION" || elmtName == "STATIC")
            {
                Caption newCaption = new Caption();
                newCaption.Load(xmlNavigator);
                return newCaption;
            }
            else if(elmtName == "IMAGE" || elmtName == "TOPIMAGE" || elmtName == "MIDDLEIMAGE" || elmtName == "BOTTOMIMAGE")
            {
                Image newImage = new Image();
                newImage.Load(xmlNavigator);
                return newImage;
            }
            else if(elmtName == "COMBOBOX" || elmtName == "JCOMBOBOX")
            {
                ComboBox newComboBox = new ComboBox();
                newComboBox.Load(xmlNavigator);
                return newComboBox;
            }
            else if(elmtName == "BUTTON" || elmtName == "PREVBUTTON" || elmtName == "NEXTBUTTON" || elmtName == "PUSHBUTTON")
            {
                Button newButton = new Button();
                newButton.Load(xmlNavigator);
                return newButton;
            }
            else if(elmtName == "EDITBOX")
            {
                EditBox newEditBox = new EditBox();
                newEditBox.Load(xmlNavigator);
                return newEditBox;
            }
            else if(elmtName == "TABBEDPANE")
            {
                TabbedPane newTabbedPane = new TabbedPane();
                newTabbedPane.Load(xmlNavigator);
                return newTabbedPane;
            }
            else if(elmtName == "TAB")
            {
                Tab newTab = new Tab();
                newTab.Load(xmlNavigator);
                return newTab;
            }
            else if (elmtName == "TABBUTTON")
            {
                TabbedButton newTabbedButton = new TabbedButton();
                newTabbedButton.Load(xmlNavigator);
                return newTabbedButton;
            }
            else if(elmtName == "RADIOBUTTON")
            {
                RadioButton newRadioButton = new RadioButton();
                newRadioButton.Load(xmlNavigator);
                return newRadioButton;
            }
            else if(elmtName == "GUAGE")
            {
                Guage newGuage = new Guage();
                newGuage.Load(xmlNavigator);
                return newGuage; 
            }
            else if(elmtName == "LISTBOX")
            {
                ListBox newListBox = new ListBox();
                newListBox.Load(xmlNavigator);
                return newListBox;
            }
            else if(elmtName == "SCROLLBAR")
            {
                ScrollBar newScrollBar = new ScrollBar();
                newScrollBar.Load(xmlNavigator);
                return newScrollBar;
            }
            else if(elmtName == "SCROLLBOX")
            {
                ScrollBox newScrollBox = new ScrollBox();
                newScrollBox.Load(xmlNavigator);
                return newScrollBox;
            }
            else if(elmtName == "CHECKBOX")
            {
                CheckBox newCheckBox = new CheckBox();
                newCheckBox.Load(xmlNavigator);
                return newCheckBox;
            }
            else if(elmtName == "JLISTBOX")
            {
                JListBox newJListBox = new JListBox();
                newJListBox.Load(xmlNavigator);
                return newJListBox;
            }
            else if(elmtName == "ZLISTBOX")
            {
                ZListBox newZListBox = new ZListBox();
                newZListBox.Load(xmlNavigator);
                return newZListBox;
            }
            else if(elmtName == "PANE")
            {
                Pane newPane = new Pane();
                newPane.Load(xmlNavigator);
                return newPane;
            }
            else if(elmtName == "TABLE")
            {
                Table newTable = new Table();
                newTable.Load(xmlNavigator);
                return newTable;
            }
            else
            {
                StatusManager.AddLog("Unknow element in xml : " + elmtName);
                return null;
            }
        }
    }
}
