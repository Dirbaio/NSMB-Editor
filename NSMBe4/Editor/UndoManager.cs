/*
*   This file is part of NSMB Editor 5.
*
*   NSMB Editor 5 is free software: you can redistribute it and/or modify
*   it under the terms of the GNU General Public License as published by
*   the Free Software Foundation, either version 3 of the License, or
*   (at your option) any later version.
*
*   NSMB Editor 5 is distributed in the hope that it will be useful,
*   but WITHOUT ANY WARRANTY; without even the implied warranty of
*   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
*   GNU General Public License for more details.
*
*   You should have received a copy of the GNU General Public License
*   along with NSMB Editor 5.  If not, see <http://www.gnu.org/licenses/>.
*/

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace NSMBe4
{
    public class UndoManager
    {
        private ToolStripSplitButton undo;
        private ToolStripSplitButton redo;
        private LevelEditorControl EdControl;

        private int undoPos;
        public List<UndoEntry> Entries;
        public bool merge = true;
        public int multiselect = 0;

        private static int actionCount;

        public UndoManager(ToolStripSplitButton UndoButton, ToolStripSplitButton RedoButton, LevelEditorControl editor)
        {
            undo = UndoButton;
            redo = RedoButton;
            EdControl = editor;

            undo.ButtonClick += new EventHandler(onUndoLast);
            redo.ButtonClick += new EventHandler(onRedoLast);

            undoPos = -1;
            Entries = new List<UndoEntry>();
        }

        public void PerformAction(UndoType undotype, object param1, object param2)
        {
            if (undotype == UndoType.ChangeSpriteData)
                undotype = UndoType.ChangeSpriteData;
            if (merge && undoPos > -1 && undoPos < Entries.Count && Array.IndexOf(combineActions, undotype) > -1 && undotype == Entries[undoPos].undotype) {
                if (undotype == UndoType.MoveMultiple) {
                    Point pt = (Point)Entries[undoPos].param2;
                    pt.X += ((Point)param2).X;
                    pt.Y += ((Point)param2).Y;
                    Entries[undoPos].param2 = pt;
                } else
                    Entries[undoPos].param2 = new Rectangle(((Rectangle)Entries[undoPos].param2).Location, ((Rectangle)param2).Size);
            } else {
                if (Entries.Count > 0)
                    Entries.Insert(undoPos, new UndoEntry(undotype, param1, param2, EdControl));
                else
                    Entries.Add(new UndoEntry(undotype, param1, param2, EdControl));
                if (undoPos == -1)
                    undoPos = 0;
                ToolStripMenuItem item = Entries[undoPos].AddToDropDown(undo.DropDownItems);
                item.MouseEnter += new EventHandler(updateActCount);
                item.Click += new EventHandler(onUndoActions);
                undo.Enabled = true;
            }
            merge = true;
            redo.DropDownItems.Clear();
            redo.Enabled = false;
            if (undoPos > 0) {
                for (int l = undoPos - 1; l >= 0; l--){
                    Entries.RemoveAt(l);
                    undoPos--;
                }
            }
        }

        private UndoType[] combineActions = { UndoType.MoveObject, UndoType.MoveSprite, UndoType.MoveMultiple, UndoType.MoveEntrance, UndoType.MoveView,
                                              UndoType.MoveZone,UndoType.SizeObject, UndoType.SizeView, UndoType.SizeZone, UndoType.MovePathNode};

        private void onUndoLast(object sender, EventArgs e)
        {
            UndoLast(false);
        }

        public void UndoLast(bool multiple)
        {
            if (undoPos > -1 && undoPos < Entries.Count) {
                undo.DropDownItems.RemoveAt(0);
                ToolStripMenuItem item = Entries[undoPos].AddToDropDown(redo.DropDownItems);
                Entries[undoPos++].DoAction(false, multiple);
                item.MouseEnter += new EventHandler(updateActCount);
                item.Click += new EventHandler(onRedoActions);
                if (undoPos == Entries.Count)
                    undo.Enabled = false;
                redo.Enabled = true;
            }
        }

        private void onUndoActions(object sender, EventArgs e)
        {
            UndoActions(actionCount);
        }

        public void UndoActions(int count)
        {
            for (int l = 0; l < count; l++) {
                UndoLast(l < count - 1);
            }
        }

        private void onRedoLast(object sender, EventArgs e)
        {
            RedoLast(false);
        }

        public void RedoLast(bool multiple)
        {
            if (undoPos > 0 && undoPos <= Entries.Count ) {
                Entries[--undoPos].DoAction(true, multiple);
                redo.DropDownItems.RemoveAt(0);
                ToolStripMenuItem item = Entries[undoPos].AddToDropDown(undo.DropDownItems);
                item.MouseEnter += new EventHandler(updateActCount);
                item.Click += new EventHandler(onUndoActions);
                if (undoPos == 0)
                    redo.Enabled = false;
                undo.Enabled = true;
            }
        }

        private void onRedoActions(object sender, EventArgs e)
        {
            RedoActions(actionCount);
        }

        public void RedoActions(int count)
        {
            for (int l = 0; l < count; l++) {
                RedoLast(l < count - 1);
            }
        }

        private void updateActCount(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            actionCount = (item.OwnerItem as ToolStripSplitButton).DropDownItems.IndexOf(item) + 1;
        }
    }

    public class UndoEntry
    {
        public UndoType undotype;
        public object param1;
        public object param2;
        public LevelEditorControl EdControl;

        public UndoEntry(UndoType undotype, object param1, object param2, LevelEditorControl editor)
        {
            this.undotype = undotype;
            this.param1 = param1;
            this.param2 = param2;
            this.EdControl = editor;
        }

        public void DoAction(bool redo, bool multiple)
        {
            NSMBObject o = null; if (param1 is NSMBObject) o = param1 as NSMBObject;
            NSMBSprite s = null; if (param1 is NSMBSprite) s = param1 as NSMBSprite;
            NSMBEntrance e = null; if (param1 is NSMBEntrance) e = param1 as NSMBEntrance;
            NSMBView v = null; if (param1 is NSMBView) v = param1 as NSMBView;
            NSMBPath p = null; if (param1 is NSMBPath) p = param1 as NSMBPath;
            NSMBPathPoint n = null; if (param1 is NSMBPathPoint) n = param1 as NSMBPathPoint;
            Object[] objs = null; if (param1 is Object[]) objs = param1 as Object[];

            Point pt = Point.Empty; if (param2 is Point) pt = (Point)param2;
            Rectangle rect = Rectangle.Empty; if (param2 is Rectangle) rect = (Rectangle)param2;
            Rectangle[] rects = null; if (param2 is Rectangle[]) rects = param2 as Rectangle[];
            int num = 0; if (param2 is int) num = (int)param2;
            int[] nums = null; if (param2 is int[]) nums = param2 as int[];

            switch (undotype) {
                case UndoType.AddObject:
                    if (redo)
                        EdControl.Level.Objects.Add(o);
                    else
                        EdControl.Level.Objects.Remove(o);
                    break;
                case UndoType.RemoveObject:
                    if (redo)
                        EdControl.Level.Objects.Remove(o);
                    else {
                        if (EdControl.Level.Objects.Count == 0)
                            EdControl.Level.Objects.Add(o);
                        else
                            EdControl.Level.Objects.Insert(num, o);
                    }
                    break;
                case UndoType.SizeObject:
                    if (redo) {
                        o.Width  = rect.Width;
                        o.Height = rect.Height;
                    } else {
                        o.Width  = rect.X;
                        o.Height = rect.Y;
                    }
                    o.UpdateObjCache();
                    break;
                case UndoType.MoveObject:
                    if (redo) {
                        o.X = rect.Width;
                        o.Y = rect.Height;
                    } else {
                        o.X = rect.X;
                        o.Y = rect.Y;
                    }
                    break;
                case UndoType.ChangeObjectType:
                    if (redo) {
                        o.Tileset = rect.Width;
                        o.ObjNum  = rect.Height;
                    } else {
                        o.Tileset = rect.X;
                        o.ObjNum  = rect.Y;
                    }
                    o.UpdateObjCache();
                    break;
                case UndoType.AddSprite:
                    if (redo) {
                        EdControl.Level.Sprites.Add(s);
                    } else
                        EdControl.Level.Sprites.Remove(s);
                    break;
                case UndoType.RemoveSprite:
                    if (redo)
                        EdControl.Level.Sprites.Remove(s);
                    else {
                        if (EdControl.Level.Sprites.Count == 0)
                            EdControl.Level.Sprites.Add(s);
                        else
                            EdControl.Level.Sprites.Insert(num, s);
                    }
                    break;
                case UndoType.MoveSprite:
                    if (redo) {
                        s.X = rect.Width;
                        s.Y = rect.Height;
                    } else {
                        s.X = rect.X;
                        s.Y = rect.Y;
                    }
                    break;
                case UndoType.ChangeSpriteData:
                    if (redo)
                        s.Data = (param2 as byte[][])[1].Clone() as byte[];
                    else
                        s.Data = (param2 as byte[][])[0].Clone() as byte[];
                    break;
                case UndoType.ChangeSpriteType:
                    if (redo)
                        s.Type = pt.Y;
                    else
                        s.Type = pt.X;
                    break;
                case UndoType.AddMultiple:
                    if (redo) {
                        foreach (object item in objs) {
                            if (item is NSMBObject)
                                EdControl.Level.Objects.Add(item as NSMBObject);
                            else
                                EdControl.Level.Sprites.Add(item as NSMBSprite);
                        }

                        if (EdControl.mode is ObjectsEditionMode)
                            (EdControl.mode as ObjectsEditionMode).SelectObjects(objs);
                    } else {
                        foreach (object item in objs) {
                            if (item is NSMBObject)
                                EdControl.Level.Objects.Remove(item as NSMBObject);
                            else
                                EdControl.Level.Sprites.Remove(item as NSMBSprite);
                        }
                    }
                    break;
                case UndoType.RemoveMultiple:
                    if (redo) {
                        foreach (object item in objs) {
                            if (item is NSMBObject)
                                EdControl.Level.Objects.Remove(item as NSMBObject);
                            else
                                EdControl.Level.Sprites.Remove(item as NSMBSprite);
                        }
                    } else {
                        for (int l = 0; l < objs.Length; l++) {
                            if (objs[l] is NSMBObject) {
                                if (nums[l] < EdControl.Level.Objects.Count)
                                    EdControl.Level.Objects.Insert(nums[l], objs[l] as NSMBObject);
                                else
                                    EdControl.Level.Objects.Add(objs[l] as NSMBObject);
                            } else {
                                if (nums[l] < EdControl.Level.Sprites.Count)
                                    EdControl.Level.Sprites.Insert(nums[l], objs[l] as NSMBSprite);
                                else
                                    EdControl.Level.Sprites.Add(objs[l] as NSMBSprite);
                            }
                        }
                        if (EdControl.mode is ObjectsEditionMode)
                            (EdControl.mode as ObjectsEditionMode).SelectObjects(objs);
                    }
                    break;
                case UndoType.MoveMultiple:
                    for (int l = 0; l < objs.Length; l++) {
                        if (redo) {
                            if (objs[l] is NSMBObject) {
                                (objs[l] as NSMBObject).X += pt.X;
                                (objs[l] as NSMBObject).Y += pt.Y;
                            } else {
                                (objs[l] as NSMBSprite).X += pt.X;
                                (objs[l] as NSMBSprite).Y += pt.Y;
                            }
                        } else {
                            if (objs[l] is NSMBObject) {
                                (objs[l] as NSMBObject).X -= pt.X;
                                (objs[l] as NSMBObject).Y -= pt.Y;
                            } else {
                                (objs[l] as NSMBSprite).X -= pt.X;
                                (objs[l] as NSMBSprite).Y -= pt.Y;
                            }
                        }
                    }
                    if (EdControl.mode is ObjectsEditionMode)
                        (EdControl.mode as ObjectsEditionMode).SelectObjects(objs);
                    break;
                case UndoType.AddEntrance:
                    if (redo)
                        EdControl.Level.Entrances.Add(e);
                    else
                        EdControl.Level.Entrances.Remove(e);
                    break;
                case UndoType.RemoveEntrance:
                    if (redo)
                        EdControl.Level.Entrances.Remove(e);
                    else {
                        if (EdControl.Level.Entrances.Count == 0)
                            EdControl.Level.Entrances.Add(e);
                        else
                            EdControl.Level.Entrances.Insert(num, e);
                    }
                    break;
                case UndoType.MoveEntrance:
                    if (redo) {
                        e.X = rect.Width;
                        e.Y = rect.Height;
                    } else {
                        e.X = rect.X;
                        e.Y = rect.Y;
                    }
                    break;
                case UndoType.ChangeEntranceData:
                    switch (rect.Width) {
                        case 0:
                            updateProperty(ref e.CameraX, redo, rect.X, rect.Y); break;
                        case 1:
                            updateProperty(ref e.CameraY, redo, rect.X, rect.Y); break;
                        case 2:
                            updateProperty(ref e.Number, redo, rect.X, rect.Y); break;
                        case 3:
                            updateProperty(ref e.DestArea, redo, rect.X, rect.Y); break;
                        case 4:
                            updateProperty(ref e.DestEntrance, redo, rect.X, rect.Y); break;
                        case 5:
                            updateProperty(ref e.ConnectedPipeID, redo, rect.X, rect.Y); break;
                        case 6:
                            updateProperty(ref e.EntryView, redo, rect.X, rect.Y); break;
                        case 7:
                            updateProperty(ref e.Type, redo, rect.X, rect.Y); break;
                        case 8:
                            updateProperty(ref e.Settings, redo, rect.X, rect.Y); break;
                    }
                    break;
                case UndoType.AddView:
                    if (redo)
                        EdControl.Level.Views.Add(v);
                    else
                        EdControl.Level.Views.Remove(v);
                    break;
                case UndoType.RemoveView:
                    if (redo)
                        EdControl.Level.Views.Remove(v);
                    else {
                        if (EdControl.Level.Views.Count == 0)
                            EdControl.Level.Views.Add(v);
                        else
                            EdControl.Level.Views.Insert(num, v);
                    }
                    break;
                case UndoType.MoveView:
                case UndoType.MoveZone:
                    if (redo) {
                        v.X = rect.Width;
                        v.Y = rect.Height;
                    } else {
                        v.X = rect.X;
                        v.Y = rect.Y;
                    }
                    break;
                case UndoType.SizeView:
                case UndoType.SizeZone:
                    if (redo) {
                        v.Width = rect.Width;
                        v.Height = rect.Height;
                    } else {
                        v.Width = rect.X;
                        v.Height = rect.Y;
                    }
                    break;
                case UndoType.ChangeViewData:
                    switch (rect.Width) {
                        case 0:
                            updateProperty(ref v.Number, redo, rect.X, rect.Y); break;
                        case 1:
                            updateProperty(ref v.Camera, redo, rect.X, rect.Y); break;
                        case 2:
                            updateProperty(ref v.Music, redo, rect.X, rect.Y); break;
                        case 3:
                            updateProperty(ref v.Unknown1, redo, rect.X, rect.Y); break;
                        case 4:
                            updateProperty(ref v.Unknown2, redo, rect.X, rect.Y); break;
                        case 5:
                            updateProperty(ref v.Unknown3, redo, rect.X, rect.Y); break;
                        case 6:
                            updateProperty(ref v.Lighting, redo, rect.X, rect.Y); break;
                        case 7:
                            updateProperty(ref v.FlagpoleID, redo, rect.X, rect.Y); break;
                    }
                    break;
                case UndoType.AddZone:
                    if (redo)
                        EdControl.Level.Zones.Add(v);
                    else
                        EdControl.Level.Zones.Remove(v);
                    break;
                case UndoType.RemoveZone:
                    if (redo)
                        EdControl.Level.Zones.Remove(v);
                    else {
                        if (EdControl.Level.Zones.Count == 0)
                            EdControl.Level.Zones.Add(v);
                        else
                            EdControl.Level.Zones.Insert(num, v);
                    }
                    break;
                case UndoType.ChangeZoneID:
                    if (redo)
                        v.Number = pt.Y;
                    else
                        v.Number = pt.X;
                    break;
                case UndoType.AddPath:
                    if (redo)
                        EdControl.Level.Paths.Add(p);
                    else
                        EdControl.Level.Paths.Remove(p);
                    break;
                case UndoType.RemovePath:
                    if (redo)
                        EdControl.Level.Paths.Remove(p);
                    else
                        EdControl.Level.Paths.Add(p);
                    break;
                case UndoType.AddPathNode:
                    if (redo)
                        if (n.parent.points.Count == 0)
                            n.parent.points.Add(n);
                        else
                            n.parent.points.Insert(num, n);
                    else
                        n.parent.points.Remove(n);
                    break;
                case UndoType.RemovePathNode:
                    if (redo)
                        n.parent.points.Remove(n);
                    else {
                        if (n.parent.points.Count == 0)
                            n.parent.points.Add(n);
                        else
                            n.parent.points.Insert(num, n);
                    }
                    break;
                case UndoType.MovePathNode:
                    if (redo) {
                        n.X = rect.Width;
                        n.Y = rect.Height;
                    } else {
                        n.X = rect.X;
                        n.Y = rect.Y;
                    }
                    break;
                case UndoType.ChangePathID:
                    if (redo)
                        p.id = pt.Y;
                    else
                        p.id = pt.X;
                    break;
                case UndoType.ChangePathNodeData:
                    switch (rect.Width){
                        case 0:
                            updateUShrt(ref n.Unknown1, redo, rect.X, rect.Y); break;
                        case 1:
                            updateUShrt(ref n.Unknown2, redo, rect.X, rect.Y); break;
                        case 2:
                            updateUShrt(ref n.Unknown3, redo, rect.X, rect.Y); break;
                        case 3:
                            updateUShrt(ref n.Unknown4, redo, rect.X, rect.Y); break;
                        case 4:
                            updateUShrt(ref n.Unknown5, redo, rect.X, rect.Y); break;
                        case 5:
                            updateUShrt(ref n.Unknown6, redo, rect.X, rect.Y); break;
                    }
                    break;
            }
            if (!multiple) {
                EdControl.mode.Refresh();
                if (EdControl.Level.Objects.Contains(o) || EdControl.Level.Sprites.Contains(s) || EdControl.Level.Entrances.Contains(e) || 
                    EdControl.Level.Views.Contains(v) || EdControl.Level.Zones.Contains(v) || EdControl.Level.Paths.Contains(p))
                    if (n == null || n != null && EdControl.Level.Paths.Contains(n.parent))
                        EdControl.mode.SelectObject(param1);
                if (EdControl.mode is ObjectsEditionMode && objs != null && (EdControl.Level.Objects.Contains(objs[0] as NSMBObject) || EdControl.Level.Sprites.Contains(objs[0] as NSMBSprite)))
                    (EdControl.mode as ObjectsEditionMode).SelectObjects(objs);
                EdControl.Invalidate(true);
            }
        }

        public ToolStripMenuItem AddToDropDown(ToolStripItemCollection DropDown)
        {
            string text = LanguageManager.GetList("UndoActions")[(int)this.undotype];
            if (undotype == UndoType.AddMultiple || undotype == UndoType.RemoveMultiple || undotype == UndoType.MoveMultiple)
                text = string.Format(text, (param1 as object[]).Length);
            if (undotype == UndoType.ChangeEntranceData)
                text = string.Format(text, LanguageManager.Get("EntranceEditor", ((Rectangle)param2).Width + ((Rectangle)param2).Height + 5).Replace(":", ""));
            if (undotype == UndoType.ChangeViewData)
                text = string.Format(text, LanguageManager.Get("ViewEditor", ((Rectangle)param2).Width + 7).Replace(":", ""));
            if (undotype == UndoType.ChangePathNodeData)
                text = string.Format(text, LanguageManager.Get("PathEditor", ((Rectangle)param2).Width + 7).Replace(":", ""));
            ToolStripMenuItem item = new ToolStripMenuItem(text);
            if (DropDown.Count > 0)
                DropDown.Insert(0, item);
            else
                DropDown.Add(item);
            return item;
        }

        private void updateProperty(ref int Prop, bool redo, int undoval, int redoval) {
            if (redo)
                Prop = redoval;
            else
                Prop = undoval;
        }

        private void updateUShrt(ref ushort Prop, bool redo, int undoval, int redoval) {
            if (redo)
                Prop = (ushort)redoval;
            else
                Prop = (ushort)undoval;
        }
    }

    public enum UndoType : int
    {
        AddObject,
        RemoveObject,
        SizeObject,
        MoveObject,
        ChangeObjectType,

        AddSprite,
        RemoveSprite,
        MoveSprite,
        ChangeSpriteData,
        ChangeSpriteType,

        AddMultiple,
        RemoveMultiple,
        MoveMultiple,

        AddEntrance,
        RemoveEntrance,
        MoveEntrance,
        ChangeEntranceData,

        AddPath,
        RemovePath,
        AddPathNode,
        RemovePathNode,
        MovePathNode,
        ChangePathID,
        ChangePathNodeData,

        AddView,
        RemoveView,
        SizeView,
        MoveView,
        ChangeViewData,

        AddZone,
        RemoveZone,
        SizeZone,
        MoveZone,
        ChangeZoneID,
    }
}
