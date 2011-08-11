using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
namespace Rose_online_UI_Editor.Files_Handlers
{
    class UI
    {
        XmlTextReader xmlReader;
        XmlWriter xmlWriter;
        private string Path;
        public Root_Element root_Element { get; set; }             
        
        public void Load(string filePath)
        {
            this.Path = filePath;
            xmlReader = new XmlTextReader(filePath);
            xmlReader.Read();
            if (xmlReader.Name != "Root_Element") throw new Exception("Error in the header");
            root_Element = new Root_Element();
            for (int i = 0; i < xmlReader.AttributeCount; i++)
            {
                xmlReader.MoveToAttribute(i);
                if (xmlReader.Name == "X")
                {
                    root_Element.x = int.Parse(xmlReader.GetAttribute(i));
                }
                if (xmlReader.Name == "Y")
                {
                    root_Element.y = int.Parse(xmlReader.GetAttribute(i));
                }
                if (xmlReader.Name == "WIDTH")
                {
                    root_Element.width = int.Parse(xmlReader.GetAttribute(i));
                }
                if (xmlReader.Name == "HEIGHT")
                {
                    root_Element.height = int.Parse(xmlReader.GetAttribute(i));
                }
                if (xmlReader.Name == "SHOWSID")
                {
                    root_Element.showsid = int.Parse(xmlReader.GetAttribute(i));
                }
                if (xmlReader.Name == "HIDESID")
                {
                    root_Element.hidesid = int.Parse(xmlReader.GetAttribute(i));
                }
                if (xmlReader.Name == "DEFAULT_X")
                {
                    root_Element.default_x = int.Parse(xmlReader.GetAttribute(i));
                }
                if (xmlReader.Name == "DEFAULT_Y")
                {
                    root_Element.default_y = int.Parse(xmlReader.GetAttribute(i));
                }
                if (xmlReader.Name == "ADJUST_X")
                {
                    root_Element.adjust_x = int.Parse(xmlReader.GetAttribute(i));
                }
                if (xmlReader.Name == "ADJUST_Y")
                {
                    root_Element.adjust_y = int.Parse(xmlReader.GetAttribute(i));
                }
            }
            while (xmlReader.Read())
            {
            
                if(xmlReader.Name == "//Root_Element") return ;
                if (xmlReader.Name == "IMAGE" && xmlReader.HasAttributes)
                {
                    Root_Element.Image Image = new Root_Element.Image();
                    Image.Load(xmlReader);
                    root_Element.listImage.Add(Image);

                }

                else if (xmlReader.Name == "BUTTON" && xmlReader.HasAttributes)
                {
                    Root_Element.Button Button = new Root_Element.Button();
                    Button.Load(xmlReader);
                    root_Element.listButton.Add(Button);
                }
                else if (xmlReader.Name == "CHECKBOX" && xmlReader.HasAttributes)
                {
                    Root_Element.CheckBox CheckBox = new Root_Element.CheckBox();
                    CheckBox.Load(xmlReader);                                       
                    root_Element.listCheckBox.Add(CheckBox);
                }
                else if (xmlReader.Name == "EDITBOX" && xmlReader.HasAttributes)
                {
                    Root_Element.EditBox EditBox = new Root_Element.EditBox();
                    EditBox.Load(xmlReader);
                    
                    root_Element.listEditBox.Add(EditBox);
                }
                else if (xmlReader.Name == "RADIOBOX" && xmlReader.HasAttributes )
                {
                    Root_Element.RadioBox RadioBox = new Root_Element.RadioBox();
                    RadioBox.Load(xmlReader);                
                    this.root_Element.listRadioBox.Add(RadioBox);
                }
                else if (xmlReader.Name == "RADIOBUTTON" && xmlReader.HasAttributes)
                {
                    Root_Element.RadioButton RadioButton = new Root_Element.RadioButton();
                    RadioButton.Load(xmlReader);
                    foreach (Root_Element.RadioBox radiobox in root_Element.listRadioBox)
                    {
                        if (radiobox.id == RadioButton.radioboxid)
                        {
                           radiobox.listRadioButton.Add(RadioButton);
                        }
                     }
                    }
                else if (xmlReader.Name == "CAPTION" && xmlReader.HasAttributes)
                {
                    Root_Element.Caption Caption = new Root_Element.Caption();
                    Caption.Load(xmlReader);
                    this.root_Element.listCaption.Add(Caption);
                }
                else if (xmlReader.Name == "ZLISTBOX" && xmlReader.HasAttributes)
                {
                    Root_Element.ZListBox ZListBox = new Root_Element.ZListBox();
                    ZListBox.Load(xmlReader);
                    this.root_Element.listZListBox.Add(ZListBox);
                }
                else if (xmlReader.Name == "LISTBOX" && xmlReader.HasAttributes)
                {
                    Root_Element.ListBox ListBox = new Root_Element.ListBox();
                    ListBox.Load(xmlReader);
                    this.root_Element.listListBox.Add(ListBox);
                }
                else if (xmlReader.Name == "SCROLLBAR" && xmlReader.HasAttributes)
                {
                    Root_Element.ScrollBar ScrollBar = new Root_Element.ScrollBar();
                    ScrollBar.Load(xmlReader);
                    this.root_Element.listScrollBar.Add(ScrollBar);
                }
                else if (xmlReader.Name == "TABBEDPANE" && xmlReader.HasAttributes)
                {
                    Root_Element.TabbedPanne TabbedPanne = new Root_Element.TabbedPanne();
                    TabbedPanne.Load(xmlReader);
                    this.root_Element.listTabbedPanne.Add(TabbedPanne);
                }
                


            }
            
        xmlReader.Close();
        }
                
        public void Reload()
        {
            Load(Path);
        }
        
        public void Save()
        {
        }
        public void Save(string filePath)
        {
        }
#region Element
      public class Root_Element
       {
           public int x { get; set; }
           public int y { get; set; }
           public int width { get; set; }
           public int height { get; set; }
           public int showsid { get; set; }
           public int hidesid { get; set; }
           public int default_x { get; set; }
           public int default_y { get; set; }
           public int adjust_x { get; set; }
           public int adjust_y { get; set; }
           public List<Image> listImage;
           public List<EditBox> listEditBox;
           public List<Button> listButton;
           public List<CheckBox> listCheckBox;
           public List<RadioBox> listRadioBox;
           public List<Caption> listCaption;
           public List<ZListBox> listZListBox;
           public List<ListBox> listListBox;
           public List<ScrollBar> listScrollBar;
           public List<TabbedPanne> listTabbedPanne;

           public Root_Element()
           {
               
               listImage = new List<Image>();
               listEditBox = new List<EditBox>();
               listButton = new List<Button>();
               listCheckBox = new List<CheckBox>();
               listRadioBox = new List<RadioBox>();
               listCaption = new List<Caption>();
               listZListBox = new List<ZListBox>();
               listListBox = new List<ListBox>();
               listScrollBar = new List<ScrollBar>();
               listTabbedPanne = new List<TabbedPanne>();
           }
           public class Image
           {
               public int id { get; set; }
               public int x { get; set; }
               public int y { get; set; }
               public int offsetx { get; set; }
               public int offsety { get; set; }
               public int width { get; set; }
               public int height { get; set; }
               public string gid { get; set; }
               public short moduleid { get; set; }
               public void Load(XmlReader xmlReader)
               {
                   for (int i = 0; i < xmlReader.AttributeCount; i++)
                   {
                       xmlReader.MoveToAttribute(i);
                       if (xmlReader.Name == "ID")
                       {
                           this.id = int.Parse(xmlReader.GetAttribute(i));
                       }
                       if (xmlReader.Name == "X")
                       {
                           this.x = int.Parse(xmlReader.GetAttribute(i));
                       }
                       if (xmlReader.Name == "Y")
                       {
                           this.y = int.Parse(xmlReader.GetAttribute(i));
                       }
                       if (xmlReader.Name == "WIDTH")
                       {
                           this.width = int.Parse(xmlReader.GetAttribute(i));
                       }
                       if (xmlReader.Name == "HEIGHT")
                       {
                           this.height = int.Parse(xmlReader.GetAttribute(i));
                       }
                       if (xmlReader.Name == "GID")
                       {
                           this.gid = xmlReader.GetAttribute(i);
                       }
                       if (xmlReader.Name == "MODULEID")
                       {
                           this.moduleid = short.Parse(xmlReader.GetAttribute(i));
                       }
                       if (xmlReader.Name == "OFFSETX")
                       {
                           this.offsetx = short.Parse(xmlReader.GetAttribute(i));
                       }
                       if (xmlReader.Name == "OFFSETY")
                       {
                           this.offsety = short.Parse(xmlReader.GetAttribute(i));
                       }

                   }

               }
           }
           public class EditBox
           {
               public int id { get; set; }
               public int x { get; set; }
               public int y { get; set; }
               public int width { get; set; }
               public int height { get; set; }
               public int charwidth { get; set; }
               public bool password { get; set; }
               public short limittext { get; set; }
               public string Name { get; set; }
               public void Load(XmlReader xmlReader)
               {
                   for (int i = 0; i < xmlReader.AttributeCount; i++)
                   {
                       xmlReader.MoveToAttribute(i);
                       if (xmlReader.Name == "ID")
                       {
                           this.id = int.Parse(xmlReader.GetAttribute(i));
                       }
                       if (xmlReader.Name == "X")
                       {
                           this.x = int.Parse(xmlReader.GetAttribute(i));
                       }
                       if (xmlReader.Name == "Y")
                       {
                           this.y = int.Parse(xmlReader.GetAttribute(i));
                       }
                       if (xmlReader.Name == "WIDTH")
                       {
                           this.width = int.Parse(xmlReader.GetAttribute(i));
                       }
                       if (xmlReader.Name == "HEIGHT")
                       {
                           this.height = int.Parse(xmlReader.GetAttribute(i));
                       }
                       if (xmlReader.Name == "CHARWIDTH")
                       {
                           this.charwidth = int.Parse(xmlReader.GetAttribute(i));
                       }
                       if (xmlReader.Name == "PASSWORD")
                       {
                           this.password = Convert.ToBoolean(int.Parse(xmlReader.GetAttribute(i)));
                       }
                       if (xmlReader.Name == "LIMITTEXT")
                       {
                           this.limittext = short.Parse(xmlReader.GetAttribute(i));
                       }
                   }
                   this.Name = xmlReader.ReadElementString();
               }
           }
           public class Button
           {
               public int id { get; set; }
               public int x { get; set; }
               public int y { get; set; }
               public int offsetx { get; set; }
               public int offsety { get; set; }

               public int width { get; set; }
               public int height { get; set; }
               public string normalgid { get; set; }
               public string overgid { get; set; }
               public string downgid { get; set; }
               public short moduleid { get; set; }
               public short clicksid { get; set; }
               public string Name { get; set; }
               public void Load(XmlReader xmlReader)
               {
                   for (int i = 0; i < xmlReader.AttributeCount; i++)
                   {
                       xmlReader.MoveToAttribute(i);
                       if (xmlReader.Name == "ID")
                       {
                           this.id = int.Parse(xmlReader.GetAttribute(i));
                       }
                       if (xmlReader.Name == "X")
                       {
                           this.x = int.Parse(xmlReader.GetAttribute(i));
                       }
                       if (xmlReader.Name == "Y")
                       {
                           this.y = int.Parse(xmlReader.GetAttribute(i));
                       }
                       if (xmlReader.Name == "WIDTH")
                       {
                           this.width = int.Parse(xmlReader.GetAttribute(i));
                       }
                       if (xmlReader.Name == "HEIGHT")
                       {
                           this.height = int.Parse(xmlReader.GetAttribute(i));
                       }
                       if (xmlReader.Name == "NORMALGID")
                       {
                           this.normalgid = xmlReader.GetAttribute(i);
                       }
                       if (xmlReader.Name == "OVERGID")
                       {
                           this.overgid = xmlReader.GetAttribute(i);
                       }
                       if (xmlReader.Name == "DOWNGID")
                       {
                           this.downgid = xmlReader.GetAttribute(i);
                       }
                       if (xmlReader.Name == "MODULEID")
                       {
                           this.moduleid = short.Parse(xmlReader.GetAttribute(i));
                       }
                       if (xmlReader.Name == "CLICKSID")
                       {
                           this.clicksid = short.Parse(xmlReader.GetAttribute(i));
                       }
                       if (xmlReader.Name == "OFFSETX")
                       {
                           this.offsetx = short.Parse(xmlReader.GetAttribute(i));
                       }
                       if (xmlReader.Name == "OFFSETY")
                       {
                           this.offsety = short.Parse(xmlReader.GetAttribute(i));
                       }
                   }

                   this.Name = xmlReader.ReadElementString();
               }
           }
           public class CheckBox
           {
               public int id { get; set; }
               public int x { get; set; }
               public int y { get; set; }
               public int width { get; set; }
               public int height { get; set; }
               public short moduleid { get; set; }
               public string checkgid { get; set; }
               public string uncheckgid { get; set; }
               public void Load(XmlReader xmlReader)
               {
                   for (int i = 0; i < xmlReader.AttributeCount; i++)
                   {
                       xmlReader.MoveToAttribute(i);
                       if (xmlReader.Name == "ID")
                       {
                           this.id = int.Parse(xmlReader.GetAttribute(i));
                       }
                       if (xmlReader.Name == "X")
                       {
                           this.x = int.Parse(xmlReader.GetAttribute(i));
                       }
                       if (xmlReader.Name == "Y")
                       {
                           this.y = int.Parse(xmlReader.GetAttribute(i));
                       }
                       if (xmlReader.Name == "WIDTH")
                       {
                           this.width = int.Parse(xmlReader.GetAttribute(i));
                       }
                       if (xmlReader.Name == "HEIGHT")
                       {
                           this.height = int.Parse(xmlReader.GetAttribute(i));
                       }
                       if (xmlReader.Name == "MODULEID")
                       {
                           this.moduleid = short.Parse(xmlReader.GetAttribute(i));
                       }
                       if (xmlReader.Name == "CHECKGID")
                       {
                           this.checkgid = xmlReader.GetAttribute(i);
                       }
                       if (xmlReader.Name == "UNCHECKGID")
                       {
                           this.uncheckgid = xmlReader.GetAttribute(i);
                       }
                   }
               }
           }
           public class RadioBox
           {
               public int id { get; set; }
               public List<RadioButton> listRadioButton = new List<RadioButton>();
               public void Load(XmlReader xmlReader)
               {
                   for (int i = 0; i < xmlReader.AttributeCount; i++)
                   {
                       xmlReader.MoveToAttribute(i);
                       if (xmlReader.Name == "ID")
                       {
                           this.id = int.Parse(xmlReader.GetAttribute(i));
                       }
                   }
               }
           }
           public class RadioButton
           {
               public int id { get; set; }
               public int x { get; set; }
               public int y { get; set; }
               public int width { get; set; }
               public int height { get; set; }
               public string normalgid { get; set; }
               public string overgid { get; set; }
               public string downgid { get; set; }
               public short moduleid { get; set; }
               public int radioboxid { get; set; }
               public string Name { get; set; }
               public void Load(XmlReader xmlReader)
               {
                   for (int i = 0; i < xmlReader.AttributeCount; i++)
                   {
                       xmlReader.MoveToAttribute(i);
                       if (xmlReader.Name == "ID")
                       {
                           this.id = int.Parse(xmlReader.GetAttribute(i));
                       }
                       if (xmlReader.Name == "X")
                       {
                           this.x = int.Parse(xmlReader.GetAttribute(i));
                       }
                       if (xmlReader.Name == "Y")
                       {
                           this.y = int.Parse(xmlReader.GetAttribute(i));
                       }
                       if (xmlReader.Name == "WIDTH")
                       {
                           this.width = int.Parse(xmlReader.GetAttribute(i));
                       }
                       if (xmlReader.Name == "HEIGHT")
                       {
                           this.height = int.Parse(xmlReader.GetAttribute(i));
                       }
                       if (xmlReader.Name == "NORMALGID")
                       {
                           this.normalgid = xmlReader.GetAttribute(i);
                       }
                       if (xmlReader.Name == "OVERGID")
                       {
                           this.overgid = xmlReader.GetAttribute(i);
                       }
                       if (xmlReader.Name == "DOWNGID")
                       {
                           this.downgid = xmlReader.GetAttribute(i);
                       }
                       if (xmlReader.Name == "MODULEID")
                       {
                           this.moduleid = short.Parse(xmlReader.GetAttribute(i));
                       }
                       if (xmlReader.Name == "RADIOBOXID")
                       {
                           this.radioboxid = int.Parse(xmlReader.GetAttribute(i));
                       }

                   }

                   this.Name = xmlReader.ReadElementString();
               }
           }
           public class Caption
           {
               public int id { get; set; }
               public int x { get; set; }
               public int y { get; set; }
               public int width { get; set; }
               public int height { get; set; }
               public void Load(XmlReader xmlReader)
               {
                   for (int i = 0; i < xmlReader.AttributeCount; i++)
                   {
                       xmlReader.MoveToAttribute(i);
                       if (xmlReader.Name == "ID")
                       {
                           this.id = int.Parse(xmlReader.GetAttribute(i));
                       }
                       if (xmlReader.Name == "X")
                       {
                           this.x = int.Parse(xmlReader.GetAttribute(i));
                       }
                       if (xmlReader.Name == "Y")
                       {
                           this.y = int.Parse(xmlReader.GetAttribute(i));
                       }
                       if (xmlReader.Name == "WIDTH")
                       {
                           this.width = int.Parse(xmlReader.GetAttribute(i));
                       }
                       if (xmlReader.Name == "HEIGHT")
                       {
                           this.height = int.Parse(xmlReader.GetAttribute(i));
                       }


                   }
               }
           }
           public class ZListBox
           {
               public int id { get; set; }
               public int x { get; set; }
               public short extent { get; set; }
               public int y { get; set; }
               public int width { get; set; }
               public int height { get; set; }
               public ScrollBar scrollbar = new ScrollBar();
               public void Load(XmlReader xmlReader)
               {
                   for (int i = 0; i < xmlReader.AttributeCount; i++)
                   {
                       xmlReader.MoveToAttribute(i);
                       if (xmlReader.Name == "ID")
                       {
                           this.id = int.Parse(xmlReader.GetAttribute(i));
                       }
                       if (xmlReader.Name == "X")
                       {
                           this.x = int.Parse(xmlReader.GetAttribute(i));
                       }
                       if (xmlReader.Name == "Y")
                       {
                           this.y = int.Parse(xmlReader.GetAttribute(i));
                       }
                       if (xmlReader.Name == "EXTENT")
                       {
                           this.extent = short.Parse(xmlReader.GetAttribute(i));
                       }
                       if (xmlReader.Name == "WIDTH")
                       {
                           this.width = int.Parse(xmlReader.GetAttribute(i));
                       }
                       if (xmlReader.Name == "HEIGHT")
                       {
                           this.height = int.Parse(xmlReader.GetAttribute(i));
                       }

                   }
               }
           }
           public class ListBox
           {
               public int id { get; set; }
               public int x { get; set; }
               public short extent { get; set; }
               public int y { get; set; }
               public int width { get; set; }
               public int height { get; set; }
               public int linespace { get; set; }
               public bool selectable { get; set; }
               public short charwidht { get; set; }
               public short charheight { get; set; }
               public short maxsize { get; set; }
               public short font { get; set; }
               public ScrollBar scrollbar = new ScrollBar();
               public void Load(XmlReader xmlReader)
               {
                   for (int i = 0; i < xmlReader.AttributeCount; i++)
                   {
                       xmlReader.MoveToAttribute(i);
                       if (xmlReader.Name == "ID")
                       {
                           this.id = int.Parse(xmlReader.GetAttribute(i));
                       }
                       if (xmlReader.Name == "X")
                       {
                           this.x = int.Parse(xmlReader.GetAttribute(i));
                       }
                       if (xmlReader.Name == "Y")
                       {
                           this.y = int.Parse(xmlReader.GetAttribute(i));
                       }
                       if (xmlReader.Name == "EXTENT")
                       {
                           this.extent = short.Parse(xmlReader.GetAttribute(i));
                       }
                       if (xmlReader.Name == "WIDTH")
                       {
                           this.width = int.Parse(xmlReader.GetAttribute(i));
                       }
                       if (xmlReader.Name == "HEIGHT")
                       {
                           this.height = int.Parse(xmlReader.GetAttribute(i));
                       }
                       if (xmlReader.Name == "LINESPACE")
                       {
                           this.linespace = int.Parse(xmlReader.GetAttribute(i));
                       }
                       if (xmlReader.Name == "SELECTABLE")
                       {
                           this.selectable = Convert.ToBoolean(int.Parse(xmlReader.GetAttribute(i)));
                       }
                       if (xmlReader.Name == "CHARWIDHT")
                       {
                           this.charwidht = short.Parse(xmlReader.GetAttribute(i));
                       }
                       if (xmlReader.Name == "CHARHEIGHT")
                       {
                           this.charheight = short.Parse(xmlReader.GetAttribute(i));
                       }
                       if (xmlReader.Name == "MAXSIZE")
                       {
                           this.maxsize = short.Parse(xmlReader.GetAttribute(i));
                       }
                       if (xmlReader.Name == "FONT")
                       {
                           this.font = short.Parse(xmlReader.GetAttribute(i));
                       }
                   }
               }
           }
           public class ScrollBar
           {
               public int id { get; set; }
               public int x { get; set; }
               public short listboxid { get; set; }
               public int y { get; set; }
               public int width { get; set; }
               public int height { get; set; }
               public ScrollBox scrollbox { get; set; }
               public class ScrollBox
               {
                   public int id { get; set; }
                   public int width { get; set; }
                   public int height { get; set; }
                   public string gid { get; set; }
                   public int moduleid { get; set; }
                   public bool blink { get; set; }
                   public int blinkmid { get; set; }
                   public string blinkgid { get; set; }
                   public int blinkswaptime { get; set; }
                   public void Load(XmlReader xmlReader)
                   {

                       for (int i = 0; i < xmlReader.AttributeCount; i++)
                       {
                           xmlReader.MoveToAttribute(i);
                           if (xmlReader.Name == "ID")
                           {
                               this.id = int.Parse(xmlReader.GetAttribute(i));
                           }

                           if (xmlReader.Name == "WIDTH")
                           {
                               this.width = int.Parse(xmlReader.GetAttribute(i));
                           }
                           if (xmlReader.Name == "HEIGHT")
                           {
                               this.height = int.Parse(xmlReader.GetAttribute(i));
                           }
                           if (xmlReader.Name == "GID")
                           {
                               this.gid = xmlReader.GetAttribute(i);
                           }
                           if (xmlReader.Name == "MODULEID")
                           {
                               this.moduleid = int.Parse(xmlReader.GetAttribute(i));
                           }
                           if (xmlReader.Name == "BLINK")
                           {
                               this.blink = Convert.ToBoolean(int.Parse(xmlReader.GetAttribute(i)));
                           }
                           if (xmlReader.Name == "BLINKMID")
                           {
                               this.blinkmid = int.Parse(xmlReader.GetAttribute(i));
                           }
                           if (xmlReader.Name == "BLINKGID")
                           {
                               this.blinkgid = xmlReader.GetAttribute(i);
                           }
                           if (xmlReader.Name == "BLINKSWAPTIME")
                           {
                               this.blinkswaptime = int.Parse(xmlReader.GetAttribute(i));
                           }
                       }
                   }
               }
               public void Load(XmlReader xmlReader)
               {
                   for (int i = 0; i < xmlReader.AttributeCount; i++)
                   {
                       xmlReader.MoveToAttribute(i);
                       if (xmlReader.Name == "ID")
                       {
                           this.id = int.Parse(xmlReader.GetAttribute(i));
                       }
                       if (xmlReader.Name == "X")
                       {
                           this.x = int.Parse(xmlReader.GetAttribute(i));
                       }
                       if (xmlReader.Name == "Y")
                       {
                           this.y = int.Parse(xmlReader.GetAttribute(i));
                       }

                       if (xmlReader.Name == "WIDTH")
                       {
                           this.width = int.Parse(xmlReader.GetAttribute(i));
                       }
                       if (xmlReader.Name == "HEIGHT")
                       {
                           this.height = int.Parse(xmlReader.GetAttribute(i));
                       }
                       if (xmlReader.Name == "LISTBOXID")
                       {
                           this.listboxid = short.Parse(xmlReader.GetAttribute(i));
                       }

                   }
                   do
                   {
                       xmlReader.Read();
                       if (xmlReader.Name == "SCROLLBOX" && xmlReader.HasAttributes)
                       {
                           ScrollBox scrollBox = new ScrollBox();
                           scrollBox.Load(xmlReader);
                           this.scrollbox = scrollBox;
                       }
                   } while (xmlReader.Name != "SCROLLBAR" && !xmlReader.HasAttributes);
               }
           }
           public class TabbedPanne
           {
               public int id { get; set; }
               public int x { get; set; }
               public int y { get; set; }
               public List<Tab> listTab = new List<Tab>();
               public class Tab
               {
                   public int id { get; set; }
                   public List<Image> listImage = new List<Image>();
                   public List<EditBox> listEditBox = new List<EditBox>();
                   public List<Button> listButton = new List<Button>();
                   public List<CheckBox> listCheckBox = new List<CheckBox>();
                   public List<RadioBox> listRadioBox = new List<RadioBox>();
                   public List<Caption> listCaption = new List<Caption>();
                   public List<ZListBox> listZListBox = new List<ZListBox>();
                   public List<ListBox> listListBox = new List<ListBox>();
                   public List<ScrollBar> listScrollBar = new List<ScrollBar>();
                   public void Load(XmlReader xmlReader)
                   {
                       for (int i = 0; i < xmlReader.AttributeCount; i++)
                       {
                           xmlReader.MoveToAttribute(i);
                           if (xmlReader.Name == "ID")
                           {
                               this.id = int.Parse(xmlReader.GetAttribute(i));
                           }
                       }
                       do
                       {
                           xmlReader.Read();
                           if (xmlReader.Name == "IMAGE" && xmlReader.HasAttributes)
                           {
                               Image image = new Image();
                               image.Load(xmlReader);
                               this.listImage.Add(image);

                           }

                           else if (xmlReader.Name == "BUTTON" && xmlReader.HasAttributes)
                           {
                               Root_Element.Button Button = new Root_Element.Button();
                               Button.Load(xmlReader);
                               this.listButton.Add(Button);
                           }
                           else if (xmlReader.Name == "CHECKBOX" && xmlReader.HasAttributes)
                           {
                               Root_Element.CheckBox CheckBox = new Root_Element.CheckBox();
                               CheckBox.Load(xmlReader);
                               this.listCheckBox.Add(CheckBox);
                           }
                           else if (xmlReader.Name == "EDITBOX" && xmlReader.HasAttributes)
                           {
                               Root_Element.EditBox EditBox = new Root_Element.EditBox();
                               EditBox.Load(xmlReader);

                               this.listEditBox.Add(EditBox);
                           }
                           else if (xmlReader.Name == "RADIOBOX" && xmlReader.HasAttributes)
                           {
                               Root_Element.RadioBox RadioBox = new Root_Element.RadioBox();
                               RadioBox.Load(xmlReader);
                               this.listRadioBox.Add(RadioBox);
                           }
                           else if (xmlReader.Name == "RADIOBUTTON" && xmlReader.HasAttributes)
                           {
                               Root_Element.RadioButton RadioButton = new Root_Element.RadioButton();
                               RadioButton.Load(xmlReader);
                               foreach (Root_Element.RadioBox radiobox in this.listRadioBox)
                               {
                                   if (radiobox.id == RadioButton.radioboxid)
                                   {
                                       radiobox.listRadioButton.Add(RadioButton);
                                   }
                               }
                           }
                           else if (xmlReader.Name == "CAPTION" && xmlReader.HasAttributes)
                           {
                               Root_Element.Caption Caption = new Root_Element.Caption();
                               Caption.Load(xmlReader);
                               this.listCaption.Add(Caption);
                           }
                           else if (xmlReader.Name == "ZLISTBOX" && xmlReader.HasAttributes)
                           {
                               Root_Element.ZListBox ZListBox = new Root_Element.ZListBox();
                               ZListBox.Load(xmlReader);
                               this.listZListBox.Add(ZListBox);
                           }
                           else if (xmlReader.Name == "LISTBOX" && xmlReader.HasAttributes)
                           {
                               Root_Element.ListBox ListBox = new Root_Element.ListBox();
                               ListBox.Load(xmlReader);
                               this.listListBox.Add(ListBox);
                           }
                           else if (xmlReader.Name == "SCROLLBAR" && xmlReader.HasAttributes)
                           {
                               Root_Element.ScrollBar ScrollBar = new Root_Element.ScrollBar();
                               ScrollBar.Load(xmlReader);
                               this.listScrollBar.Add(ScrollBar);
                           }
                       } while (xmlReader.Name != "TAB" );

                   }
               }
               public void Load(XmlReader xmlReader)
               {
                   for (int i = 0; i < xmlReader.AttributeCount; i++)
                   {
                       xmlReader.MoveToAttribute(i);
                       if (xmlReader.Name == "ID")
                       {
                           this.id = int.Parse(xmlReader.GetAttribute(i));
                       }
                       if (xmlReader.Name == "X")
                       {
                           this.x = int.Parse(xmlReader.GetAttribute(i));
                       }
                       if (xmlReader.Name == "Y")
                       {
                           this.y = int.Parse(xmlReader.GetAttribute(i));
                       }
                   }

                   do
                   {
                       xmlReader.Read();
                       if (xmlReader.Name == "TAB" && xmlReader.HasAttributes)
                       {
                           Tab tab = new Tab();
                           tab.Load(xmlReader);
                           this.listTab.Add(tab);
                       }
                   } while (xmlReader.Name != "TABBEDPANE" && !xmlReader.HasAttributes);
               }
           }

       }
#endregion
    
    }
}
