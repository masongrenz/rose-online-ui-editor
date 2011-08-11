using System;
using System.Numeric;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Microsoft.Xna;
using Microsoft.Xna.Framework;
using Rose_online_UI_Editor.Forms.CustomControls;
using Rose_online_UI_Editor.Files_Handlers;

namespace Rose_online_UI_Editor.Forms
{
    partial class MainForm : Form
    {

        #region Variables
        private Object oldSelectedObject; //Use it for Undo\Redo with propertyGrid     
        private TSI.DDS copiedDDS; //They should be here so we can do copy\paste from 2 different TSI
        private TSI.DDS.DDSElement copiedSprite; //Same
        bool TSIclick;
        bool UIclick;
        Vector2 MouseOldPosition;
        #endregion

        #region AllControl
        private void AddDockContainer<T>(T newDockContainer)
        {
            if (typeof(T) == typeof(XmlDockContainer))
            {
                MainBar.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            (XmlDockContainer)(object)newDockContainer});
            }
            else if (typeof(T) == typeof(DDSDockContainer))
            {
                MainBar.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            (DDSDockContainer)(object)newDockContainer});
            }
            if (typeof(T) == typeof(TSIDockContainer))
            {
               MainBar.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            (TSIDockContainer)(object)newDockContainer});
            }
            if (typeof(T) == typeof(UIDockContainer))
            {
                MainBar.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            (UIDockContainer)(object)newDockContainer});
            }

            MainBar.SelectedDockTab = MainBar.Items.Count - 1;
        }

        private void AddLog(string log, LogType logType)
        {
            switch (logType)
            {
                case LogType.MSG_NONE:
                    this.logTextBox.SelectionColor = Color.Black;
                    this.logTextBox.AppendText("[" + System.DateTime.Now + "] : " + log);
                    this.logTextBox.AppendText("\n");
                    break;
                case LogType.MSG_INFO:
                    this.logTextBox.SelectionColor = Color.Blue;
                    this.logTextBox.AppendText("[" + System.DateTime.Now + "] : " + log);
                    this.logTextBox.AppendText("\n");
                    break;
                case LogType.MSG_WARNING:
                    this.logTextBox.SelectionColor = Color.OrangeRed;
                    this.logTextBox.AppendText("[" + System.DateTime.Now + "] : " + log);
                    this.logTextBox.AppendText("\n");
                    break;
                case LogType.MSG_ERROR:
                    this.logTextBox.SelectionColor = Color.Red;
                    this.logTextBox.AppendText("[" + System.DateTime.Now + "] : " + log);
                    this.logTextBox.AppendText("\n");
                    break;
            }
        }
        #endregion

        #region TSIControl

        #region function
        enum LogType
        {
            MSG_NONE,
            MSG_INFO,
            MSG_WARNING,
            MSG_ERROR,
        };
        #endregion

        #region rendering

        private void TSIRenderMouseUp(object sender, MouseEventArgs e)
        {
            if (this.MainBar.Items[MainBar.SelectedDockTab].GetType() == typeof(TSIDockContainer))
            {
                TSIDockContainer SelectTSIDockContainer = (TSIDockContainer)this.MainBar.Items[MainBar.SelectedDockTab];
                if (TreeTSI.SelectedNode != null)
                {
                    if (TreeTSI.SelectedNode.Level == 1)
                    {
                        TSIclick = false;
                        propertyGrid.Refresh();
                    }
                }
             }
        }

        private void TSIRenderMouseDown(object sender, MouseEventArgs e)
        {/*
            if (this.MainBar.Items[MainBar.SelectedDockTab].GetType() == typeof(TSIDockContainer))
            {
                TSIDockContainer SelectTSIDockContainer = (TSIDockContainer)this.MainBar.Items[MainBar.SelectedDockTab];
                if (TreeTSI.SelectedNode != null)
                {
                    if (TreeTSI.SelectedNode.Level == 1)
                    {
                        int ElementIndex = TreeTSI.SelectedNode.Index;
                        int DDSIndex = TreeTSI.SelectedNode.Parent.Index;


                        if (!TSIclick)
                        {
                            TSIclick = true;
                            SelectTSIDockContainer.tsi.listDDS[DDSIndex].ListDDS_element[ElementIndex].X = e.X;
                            SelectTSIDockContainer.tsi.listDDS[DDSIndex].ListDDS_element[ElementIndex].Y = e.Y;
                            SelectTSIDockContainer.tsi.listDDS[DDSIndex].ListDDS_element[ElementIndex].Width = 1; //can't be null
                            SelectTSIDockContainer.tsi.listDDS[DDSIndex].ListDDS_element[ElementIndex].Height = 1; //can't be null

                        }

                        SelectTSIDockContainer.DrawDDS(DDSIndex);
                        SelectTSIDockContainer.DrawAera(DDSIndex, ElementIndex);
                      
                    }
                }
            }*/
        }


        private void TSIRenderMouseMove(object sender, MouseEventArgs e)
        {
           /*
            if (this.MainBar.Items[MainBar.SelectedDockTab].GetType() == typeof(TSIDockContainer))
            {
                TSIDockContainer SelectTSIDockContainer = (TSIDockContainer)this.MainBar.Items[MainBar.SelectedDockTab];
                if (TreeTSI.SelectedNode != null)
                {
                    if (TreeTSI.SelectedNode.Level == 1)
                    {
                        int ElementIndex = TreeTSI.SelectedNode.Index;
                        int DDSIndex = TreeTSI.SelectedNode.Parent.Index;

                        if (TSIclick)
                        {
                            if (e.X - SelectTSIDockContainer.tsi.listDDS[DDSIndex].ListDDS_element[ElementIndex].X > 0 && e.Y - SelectTSIDockContainer.tsi.listDDS[DDSIndex].ListDDS_element[ElementIndex].Y > 0)
                            {
                                SelectTSIDockContainer.tsi.listDDS[DDSIndex].ListDDS_element[ElementIndex].Width = e.X - SelectTSIDockContainer.tsi.listDDS[DDSIndex].ListDDS_element[ElementIndex].X;
                                SelectTSIDockContainer.tsi.listDDS[DDSIndex].ListDDS_element[ElementIndex].Height = e.Y - SelectTSIDockContainer.tsi.listDDS[DDSIndex].ListDDS_element[ElementIndex].Y;
                            }
                        }

                        SelectTSIDockContainer.DrawDDS(DDSIndex);
                        SelectTSIDockContainer.DrawAera(DDSIndex, ElementIndex);
                           
                    }
                }
             }*/
        }

        private void UIRenderMouseMove(object sender, MouseEventArgs e)
        {
            if (this.MainBar.Items[MainBar.SelectedDockTab].GetType() == typeof(UIDockContainer))
            {
                UIDockContainer SelectUIDockContainer = (UIDockContainer)this.MainBar.Items[MainBar.SelectedDockTab];
               
                if (UIclick)
                {
                    if (SelectUIDockContainer.renderControl.Cursor == Cursors.Hand)
                    {
                        SelectUIDockContainer.renderControl.cam._pos = new Vector2(SelectUIDockContainer.renderControl.cam._pos.X - (e.X - MouseOldPosition.X), SelectUIDockContainer.renderControl.cam._pos.Y - (e.Y - MouseOldPosition.Y));
                    }
                    MouseOldPosition = new Vector2(e.X, e.Y);
                }
            }
        }
        private void UIRenderMouseUp(object sender, MouseEventArgs e)
        {
            if (this.MainBar.Items[MainBar.SelectedDockTab].GetType() == typeof(UIDockContainer))
            {
                
                UIDockContainer SelectUIDockContainer = (UIDockContainer)this.MainBar.Items[MainBar.SelectedDockTab];
               UIclick = false;
            }
        }
        private void UIRenderMouseDown(object sender, MouseEventArgs e)
        {
            if (this.MainBar.Items[MainBar.SelectedDockTab].GetType() == typeof(UIDockContainer))
            {
                UIDockContainer SelectUIDockContainer = (UIDockContainer)this.MainBar.Items[MainBar.SelectedDockTab];
                if (!UIclick)
                {
                    UIclick = true;
                    MouseOldPosition = new Vector2(e.X, e.Y);
                }
            }
        }
        #endregion
        #endregion
    }
}