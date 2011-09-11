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

        public Stack<Action> UActions = new Stack<Action>();
        public Stack<Action> RActions = new Stack<Action>();
        public bool merge = true;
        public int multiselect = 0;
        public bool dirty = false;

        private static int actionCount;

        public UndoManager(ToolStripSplitButton UndoButton, ToolStripSplitButton RedoButton, LevelEditorControl editor)
        {
            undo = UndoButton;
            redo = RedoButton;
            EdControl = editor;

            undo.ButtonClick += new EventHandler(onUndoLast);
            redo.ButtonClick += new EventHandler(onRedoLast);
        }

        public void Do(Action act)
        {
            if (act.cancel)
                return;

            //First do the action. Only the *new* action
            act.SetEdControl(EdControl);
            act.Redo();
            
            //Then save the done action. Merge with previous actions if needed.
            bool merged = false;
            //Determine if the actions should be merged
            if (merge && UActions.Count > 0 && UActions.Peek().CanMerge && UActions.Peek().GetType().Equals(act.GetType()))
            {
                Action pAct = UActions.Peek();
                merged = pAct.Merge(act);
            }
 
            //If we didn't merge the action we need to save the new action.
            if (!merged) 
            {
                UActions.Push(act);
                ToolStripMenuItem item = new ToolStripMenuItem(act.ToString());
                item.MouseEnter += new EventHandler(updateActCount);
                item.Click += new EventHandler(onUndoActions);
                undo.DropDownItems.Insert(0, item);
            }

            //Clear the redo buffer because we just did a new action.
            if (redo.DropDownItems.Count > 0) {
                redo.DropDownItems.Clear();
                RActions.Clear();
            }

            //Always after doing an action.
            EdControl.repaint();

            //Now set some flags.
            undo.Enabled = true;
            redo.Enabled = false;

            merge = true;
            dirty = true;
        }

        public void Clean()
        {
            dirty = false;
        }


        //These two functions actually do the undo/redo actions.
        private void UndoLast()
        {
            if (UActions.Count > 0)
            {
                undo.DropDownItems.RemoveAt(0);
                ToolStripMenuItem item = new ToolStripMenuItem(UActions.Peek().ToString());
                item.MouseEnter += new EventHandler(updateActCount);
                item.Click += new EventHandler(onRedoActions);
                redo.DropDownItems.Insert(0, item);
                UActions.Peek().Undo();
                RActions.Push(UActions.Pop());
                undo.Enabled = undo.DropDownItems.Count > 0;
                redo.Enabled = true;
                dirty = undo.Enabled;
            }
        }
        private void RedoLast()
        {
            if (RActions.Count > 0)
            {
                redo.DropDownItems.RemoveAt(0);
                ToolStripMenuItem item = new ToolStripMenuItem(RActions.Peek().ToString());
                item.MouseEnter += new EventHandler(updateActCount);
                item.Click += new EventHandler(onUndoActions);
                undo.DropDownItems.Insert(0, item);
                RActions.Peek().Redo();
                UActions.Push(RActions.Pop());
                redo.Enabled = redo.DropDownItems.Count > 0;
                undo.Enabled = true;
                dirty = true;
            }
        }

        //Single undo/redo
        public void onUndoLast(object sender, EventArgs e)
        {
            UndoLast();
            EdControl.repaint();
        }
        public void onRedoLast(object sender, EventArgs e)
        {
            RedoLast();
            EdControl.repaint();
        }

        //Multiple undo/redo
        private void onUndoActions(object sender, EventArgs e)
        {
            for (int l = 0; l < actionCount; l++)
                UndoLast();

            EdControl.repaint();
        }
        private void onRedoActions(object sender, EventArgs e)
        {
            for (int l = 0; l < actionCount; l++)
                RedoLast();

            EdControl.repaint();
        }

        //This is called to know how many actions to undo/redo.
        //So actionCount will always hold the number of actions when onUndoActions/onRedoActions is called.
        private void updateActCount(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            actionCount = (item.OwnerItem as ToolStripSplitButton).DropDownItems.IndexOf(item) + 1;
        }

        //Helper functions? Here? :O
        public static byte[][] Clone(byte[][] data)
        {
            int len = data.GetLength(0);
            byte[][] newData = new byte[len][];
            for (int l = 0; l < len; l++)
                newData[l] = data[l].Clone() as byte[];
            return newData;
        }

        public static List<LevelItem> ObjToList(LevelItem obj)
        {
            List<LevelItem> l = new List<LevelItem>();
            l.Add(obj);
            return l;
        }
    }

    public class Action
    {
        public LevelEditorControl EdControl;
        public bool cancel;

        public Action() { }
        /*
        public void DoUndo(bool multiple)
        {
            this.Undo();
            if (this is ChangeSpriteTypeAction || !multiple) {
                this.AfterAction();
                EdControl.repaint();
            }
        }
        public void DoRedo(bool multiple)
        {
            this.Redo();
            if (this is ChangeSpriteTypeAction || !multiple) {
                this.AfterAction();
                EdControl.repaint();
            }
        }*/
        public virtual void Undo() { }
        public virtual void Redo() { }
        public virtual void AfterAction() { }
        public virtual void AfterSetEdControl() { }
        public virtual bool CanMerge {
            get {
                return false;
            }
        }

        //Merge returns true if merge has been done successfully
        public virtual bool Merge(Action act) { return false; }
//        public bool MergeFailed = false;

        public void SetEdControl(LevelEditorControl EdControl)
        {
            this.EdControl = EdControl;
            this.AfterSetEdControl();
        }
    }

    #region General Actions
    public class LvlItemAction : Action
    {
        public List<LevelItem> objs;
        public LvlItemAction(List<LevelItem> objs) {

            //This is important. We have to clone the list.
            this.objs = new List<LevelItem>();
            this.objs.AddRange(objs);

            if (objs.Count == 0)
                cancel = true;
        }
        public override void AfterAction() {
            EdControl.mode.SelectObject(objs);
        }
    }

    public class AddLvlItemAction : LvlItemAction
    {
        public AddLvlItemAction(List<LevelItem> objs) : base(objs) { }
        public override void Undo()
        {
            EdControl.Level.Remove(objs);
        }
        public override void Redo()
        {
            EdControl.Level.Add(objs);
        }
    }
    public class RemoveLvlItemAction : LvlItemAction
    {
        public RemoveLvlItemAction(List<LevelItem> objs) : base(objs) { }
        public override void Undo()
        {
            EdControl.Level.Add(objs);
        }
        public override void Redo()
        {
            EdControl.Level.Remove(objs);
        }
    }

    public class MoveLvlItemAction : LvlItemAction
    {
        int XDelta, YDelta;
        public MoveLvlItemAction(List<LevelItem> objs, int XDelta, int YDelta)
            : base(objs)
        {
            this.XDelta = XDelta;
            this.YDelta = YDelta;
        }

        /*
        public MoveLvlItemAction(List<LevelItem> objs, LevelItem BaseObj, int NewX, int NewY)
            : base(objs)
        {
            this.XDelta = NewX - BaseObj.x;
            this.YDelta = NewY - BaseObj.y;
        }*/

        public override void Undo()
        {
            foreach (LevelItem obj in objs) {
                obj.x -= XDelta;
                obj.y -= YDelta;
            }
        }
        public override void Redo()
        {
            foreach (LevelItem obj in objs) {
                obj.x += XDelta;
                obj.y += YDelta;
            }
        }
        public override bool CanMerge {
            get {
                return true;
            }
        }
        public override bool Merge(Action act)
        {
            MoveLvlItemAction mlia = act as MoveLvlItemAction;
            this.XDelta += mlia.XDelta;
            this.YDelta += mlia.YDelta;

            return true;
        }
    }

    public class ResizeLvlItemAction : LvlItemAction
    {
        int XDelta, YDelta;
        public ResizeLvlItemAction(List<LevelItem> objs, int XDelta, int YDelta)
            : base(objs)
        {
            this.XDelta = XDelta;
            this.YDelta = YDelta;
        }

        public override void Undo()
        {
            foreach (LevelItem obj in objs) {
                obj.width -= XDelta;
                obj.height -= YDelta;
                if (obj is NSMBObject)
                    (obj as NSMBObject).UpdateObjCache();
            }
        }
        public override void Redo()
        {
            foreach (LevelItem obj in objs) {
                obj.width += XDelta;
                obj.height += YDelta;
                if (obj is NSMBObject)
                    (obj as NSMBObject).UpdateObjCache();
            }
        }
        public override bool CanMerge {
            get {
                return true;
            }
        }

        public override bool Merge(Action act)
        {
            ResizeLvlItemAction rlia = act as ResizeLvlItemAction;
            this.XDelta += rlia.XDelta;
            this.YDelta += rlia.YDelta;

            return true;
        }
    }
    #endregion
    #region Specific Actions
    public class ChangeObjectTypeAction : LvlItemAction
    {
        List<int> OrigTS, OrigNum;
        int NewTS, NewNum;
        public ChangeObjectTypeAction(List<LevelItem> theobjs, int NewTS, int NewNum)
            : base(theobjs)
        {
            NSMBObject o;
            OrigTS = new List<int>();
            OrigNum = new List<int>();
            for (int l = 0; l < objs.Count; l++)
                if (objs[l] is NSMBObject) {
                    o = objs[l] as NSMBObject;
                    OrigTS.Add(o.Tileset);
                    OrigNum.Add(o.ObjNum);
                } else {
                    objs.RemoveAt(l);
                    l--;
                }
            this.NewTS = NewTS;
            this.NewNum = NewNum;
        }
        public override void Undo()
        {
            NSMBObject o;
            for (int l = 0; l < objs.Count; l++) {
                o = objs[l] as NSMBObject;
                o.Tileset = OrigTS[l];
                o.ObjNum = OrigNum[l];
                o.UpdateObjCache();
            }
        }
        public override void Redo()
        {
            NSMBObject o;
            foreach (LevelItem obj in objs) {
                o = obj as NSMBObject;
                o.Tileset = NewTS;
                o.ObjNum = NewNum;
                o.UpdateObjCache();
            }
        }
        public override bool CanMerge {
            get {
                return true;
            }
        }
        public override bool Merge(Action act)
        {
            ChangeObjectTypeAction cota = act as ChangeObjectTypeAction;
            this.NewTS = cota.NewTS;
            this.NewNum = cota.NewNum;

            return true;
        }
        public override string ToString()
        {
            return LanguageManager.GetList("UndoActions")[4];
        }
    }
    public class ChangeSpriteTypeAction : LvlItemAction
    {
        List<int> OrigType;
        int NewType;
        public ChangeSpriteTypeAction(List<LevelItem> objs, int NewType)
            : base(objs)
        {
            OrigType = new List<int>();
            for (int l = 0; l < objs.Count; l++) {
                if (objs[l] is NSMBSprite)
                    OrigType.Add((objs[l] as NSMBSprite).Type);
                else {
                    objs.RemoveAt(l);
                    l--;
                }
            }
            this.NewType = NewType;
        }
        public override void Undo()
        {
            for (int l = 0; l < objs.Count; l++)
                (objs[l] as NSMBSprite).Type = OrigType[l];
        }
        public override void Redo()
        {
            foreach (LevelItem obj in objs)
                (obj as NSMBSprite).Type = NewType;
        }
        public override bool CanMerge {
            get {
                return true;
            }
        }
        public override bool Merge(Action act)
        {
            ChangeSpriteTypeAction csta = act as ChangeSpriteTypeAction;
            this.NewType = csta.NewType;

            return true;
        }
        public override string ToString()
        {
            return LanguageManager.GetList("UndoActions")[8];
        }
    }

    //Only works on one sprite for now
    public class ChangeSpriteDataAction : LvlItemAction
    {
        byte[] OrigData, NewData;
        public ChangeSpriteDataAction(List<LevelItem> objs, byte[] NewData) 
            : base(objs)
        {
            for (int l = 0; l < objs.Count; l++)
                if (objs[l] is NSMBSprite)
                    OrigData = (objs[l] as NSMBSprite).Data.Clone() as byte[];
                else {
                    objs.RemoveAt(l);
                    l--;
                }
            for (int l = 1; l < objs.Count; l++)
                objs.RemoveAt(l);
            if (objs.Count == 0)
                cancel = true;
            this.NewData = NewData;
        }
        public override void Undo()
        {
            (objs[0] as NSMBSprite).Data = OrigData.Clone() as byte[];
        }
        public override void Redo()
        {
            (objs[0] as NSMBSprite).Data = NewData.Clone() as byte[];
        }
        public override bool CanMerge {
            get {
                return true;
            }
        }
        public override bool Merge(Action act)
        {
            ChangeSpriteDataAction csda = act as ChangeSpriteDataAction;
            this.NewData = csda.NewData;

            return true;
        }
        public override string ToString()
        {
            return LanguageManager.GetList("UndoActions")[9];
        }
    }
    public class ChangeEntranceDataAction : LvlItemAction
    {
        int PropNum;
        List<int> OrigV;
        int NewV;
        public ChangeEntranceDataAction(List<LevelItem> objs, int PropNum, int NewV)
            : base(objs)
        {
            OrigV = new List<int>();
            this.NewV = NewV;
            this.PropNum = PropNum;
            for (int l = 0; l < objs.Count; l++)
                if (objs[l] is NSMBEntrance)
                    OrigV.Add(Read(objs[l] as NSMBEntrance));
                else {
                    objs.RemoveAt(l);
                    l--;
                }
        }
        public override void Undo()
        {
            for (int l = 0; l < objs.Count; l++)
                Write(objs[l] as NSMBEntrance, OrigV[l]);
        }
        public override void Redo()
        {
            foreach (LevelItem obj in objs)
                Write(obj as NSMBEntrance, NewV);
        }
        private int Read(NSMBEntrance e)
        {
            switch (PropNum) {
                case 0: return e.CameraX;
                case 1: return e.CameraY;
                case 2: return e.Number;
                case 3: return e.DestArea;
                case 4: return e.DestEntrance;
                case 5: return e.ConnectedPipeID;
                case 6: return e.EntryView;
                case 7: return e.Type;
                case 8: return e.Settings;
            }
            return 0;
        }
        private void Write(NSMBEntrance e, int value)
        {
            switch (PropNum) {
                case 0: e.CameraX = value; break;
                case 1: e.CameraY = value; break;
                case 2: e.Number = value; break;
                case 3: e.DestArea = value; break;
                case 4: e.DestEntrance = value; break;
                case 5: e.ConnectedPipeID = value; break;
                case 6: e.EntryView = value; break;
                case 7: e.Type = value; break;
                case 8: e.Settings = value; break;
            }
        }
        public override bool CanMerge {
            get {
                return true;
            }
        }
        public override bool Merge(Action act) {
            ChangeEntranceDataAction ceda = (act as ChangeEntranceDataAction);
            if (ceda.PropNum != this.PropNum)
                return false;

            this.NewV = ceda.NewV;
            return true;
        }
        public override string ToString()
        {
            return string.Format(LanguageManager.GetList("UndoActions")[16], LanguageManager.Get("EntranceEditor", PropNum + 5).Replace(":", ""));
        }
    }
    public class ChangePathIDAction : LvlItemAction
    {
        int OrigID, NewID;
        public ChangePathIDAction(List<LevelItem> objs, int NewID)
            : base(objs)
        {
            for (int l = 0; l < objs.Count; l++)
                if (objs[l] is NSMBPathPoint) {
                    OrigID = (objs[l] as NSMBPathPoint).parent.id;
                    break;
                } else {
                    objs.RemoveAt(l);
                    l--;
                }
            for (int l = 1; l < objs.Count; l++)
                objs.RemoveAt(l);
            if (objs.Count == 0)
                this.cancel = true;
            this.NewID = NewID;
        }
        public override void Undo()
        {
            (objs[0] as NSMBPathPoint).parent.id = OrigID;
        }
        public override void Redo()
        {
            (objs[0] as NSMBPathPoint).parent.id = NewID;
        }
        public override string ToString()
        {
            return LanguageManager.GetList("UndoActions")[20];
        }
    }
    public class ChangePathNodeDataAction : LvlItemAction
    {
        int PropNum;
        List<ushort> OrigV;
        ushort NewV;
        public ChangePathNodeDataAction(List<LevelItem> objs, int PropNum, int NewV)
            : base(objs)
        {
            OrigV = new List<ushort>();
            this.PropNum = PropNum;
            this.NewV = (ushort)NewV;
            for (int l = 0; l < objs.Count; l++)
                if (objs[l] is NSMBPathPoint)
                    OrigV.Add(Read(objs[l] as NSMBPathPoint));
                else {
                    objs.RemoveAt(l);
                    l--;
                }
        }
        public override void Undo()
        {
            for (int l = 0; l < objs.Count; l++)
                Write(objs[l] as NSMBPathPoint, OrigV[l]);
        }
        public override void Redo()
        {
            foreach (LevelItem obj in objs)
                Write(obj as NSMBPathPoint, NewV);
        }
        private ushort Read(NSMBPathPoint p)
        {
            switch (PropNum) {
                case 0: return p.Unknown1;
                case 1: return p.Unknown2;
                case 2: return p.Unknown3;
                case 3: return p.Unknown4;
                case 4: return p.Unknown5;
                case 5: return p.Unknown6;
            }
            return 0;
        }
        private void Write(NSMBPathPoint p, ushort value)
        {
            switch (PropNum) {
                case 0: p.Unknown1 = value; break;
                case 1: p.Unknown2 = value; break;
                case 2: p.Unknown3 = value; break;
                case 3: p.Unknown4 = value; break;
                case 4: p.Unknown5 = value; break;
                case 5: p.Unknown6 = value; break;
            }
        }
        public override bool CanMerge {
            get {
                return true;
            }
        }
        public override bool Merge(Action act)
        {
            ChangePathNodeDataAction cpnd = (act as ChangePathNodeDataAction);
            if (cpnd.PropNum != this.PropNum)
                return false;

            this.NewV = cpnd.NewV;
            return true;
        }
        public override string ToString()
        {
            return string.Format(LanguageManager.GetList("UndoActions")[24], LanguageManager.Get("PathEditor", PropNum + 7).Replace(":", ""));
        }
    }
    public class ChangeViewDataAction : LvlItemAction
    {
        int PropNum;
        List<int> OrigV;
        int NewV;
        public ChangeViewDataAction(List<LevelItem> objs, int PropNum, int NewV)
            : base(objs)
        {
            OrigV = new List<int>();
            this.PropNum = PropNum;
            this.NewV = NewV;
            for (int l = 0; l < objs.Count; l++)
                if (objs[l] is NSMBView)
                    OrigV.Add(Read(objs[l] as NSMBView));
                else {
                    objs.RemoveAt(l);
                    l--;
                }
        }
        public override void Undo()
        {
            for (int l = 0; l < objs.Count; l++)
                Write(objs[l] as NSMBView, OrigV[l]);
        }
        public override void Redo()
        {
            foreach (LevelItem obj in objs)
                Write(obj as NSMBView, NewV);
        }
        private int Read(NSMBView v)
        {
            switch (PropNum) {
                case 1: return v.Number;
                case 2: return v.Music;
                case 3: return v.Unknown1;
                case 4: return v.Unknown2;
                case 5: return v.Unknown3;
                case 6: return v.Lighting;
                case 7: return v.FlagpoleID;
                case 8: return v.CameraTop;
                case 9: return v.CameraBottom;
                case 10: return v.CameraTopSpin;
                case 11: return v.CameraBottomSpin;
                case 12: return v.CameraBottomStick;
            }
            return 0;
        }
        private void Write(NSMBView v, int value)
        {
            switch (PropNum) {
                case 1: v.Number = value; break;
                case 2: v.Music = value; break;
                case 3: v.Unknown1 = value; break;
                case 4: v.Unknown2 = value; break;
                case 5: v.Unknown3 = value; break;
                case 6: v.Lighting = value; break;
                case 7: v.FlagpoleID = value; break;
                case 8: v.CameraTop = value; break;
                case 9: v.CameraBottom = value; break;
                case 10: v.CameraTopSpin = value; break;
                case 11: v.CameraBottomSpin= value; break;
                case 12: v.CameraBottomStick = value; break;
            }
        }
        public override bool CanMerge {
            get {
                return true;
            }
        }
        public override bool Merge(Action act)
        {
            ChangeViewDataAction cvda = (act as ChangeViewDataAction);
            if (cvda.PropNum != this.PropNum)
                return false;

            this.NewV = cvda.NewV;
            return true;
        }
        //public override string ToString()
        //{
        //    if (view.isZone)
        //        return LanguageManager.GetList("UndoActions")[34];
        //    else
        //        return string.Format(LanguageManager.GetList("UndoActions")[29], LanguageManager.Get("ViewEditor", PropNum + 6).Replace(":", ""));
        //}
    }
    #endregion
    #region Other
    public class ChangeLevelSettingsAction : Action
    {
        byte[][] oldData, newData;
        public ChangeLevelSettingsAction(byte[][] newData)
        {
            this.newData = newData;
        }
        public override void Undo()
        {
            EdControl.Level.Blocks = UndoManager.Clone(oldData);
            EdControl.Level.CalculateSpriteModifiers();
        }
        public override void Redo()
        {
            EdControl.Level.Blocks = UndoManager.Clone(newData);
            EdControl.Level.CalculateSpriteModifiers();
        }
        public override void AfterSetEdControl()
        {
            this.oldData = UndoManager.Clone(EdControl.Level.Blocks);
        }
        /*
        public override void AfterAction()
        {
            EdControl.Level.CalculateSpriteModifiers();
        }*/
        public override string ToString()
        {
            return LanguageManager.GetList("UndoActions")[36];
        }
    }
    #endregion
}
