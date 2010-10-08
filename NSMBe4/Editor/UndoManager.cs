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
            //Determine if the actions should be merged
            if (merge && UActions.Count > 0 && UActions.Peek().CanMerge && 
            UActions.Peek().GetType().Equals(act.GetType())) {
                UActions.Peek().Merge(act);
                if (act is MoveMultipleAction || act is MovePathAction) {
                    act.SetEdControl(EdControl);
                    act.DoRedo(false);
                    act = null;
                } else
                    act = UActions.Peek();
            } else {
                act.SetEdControl(EdControl);
                UActions.Push(act);
                ToolStripMenuItem item = new ToolStripMenuItem(act.ToString());
                item.MouseEnter += new EventHandler(updateActCount);
                item.Click += new EventHandler(onUndoActions);
                undo.DropDownItems.Insert(0, item);
            }
            if (redo.DropDownItems.Count > 0) {
                redo.DropDownItems.Clear();
                RActions.Clear();
            }
            undo.Enabled = true;
            redo.Enabled = false;
            if (act != null)
                act.DoRedo(false);
            merge = true;
            dirty = true;
        }
        public void Perform(Action act)
        {
            act.SetEdControl(EdControl);
            act.DoRedo(false);
        }
        public void Clean()
        {
            dirty = false;
        }
        private void onUndoLast(object sender, EventArgs e)
        {
            UndoLast(false);
        }

        public void UndoLast(bool multiple)
        {
            if (UActions.Count > 0) {
                undo.DropDownItems.RemoveAt(0);
                ToolStripMenuItem item = new ToolStripMenuItem(UActions.Peek().ToString());
                item.MouseEnter += new EventHandler(updateActCount);
                item.Click += new EventHandler(onRedoActions);
                redo.DropDownItems.Insert(0, item);
                UActions.Peek().DoUndo(multiple);
                RActions.Push(UActions.Pop());
                undo.Enabled = undo.DropDownItems.Count > 0;
                redo.Enabled = true;
                dirty = undo.Enabled;
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
            if (RActions.Count > 0) {
                redo.DropDownItems.RemoveAt(0);
                ToolStripMenuItem item = new ToolStripMenuItem(RActions.Peek().ToString());
                item.MouseEnter += new EventHandler(updateActCount);
                item.Click += new EventHandler(onUndoActions);
                undo.DropDownItems.Insert(0, item);
                RActions.Peek().DoRedo(multiple);
                UActions.Push(RActions.Pop());
                redo.Enabled = redo.DropDownItems.Count > 0;
                undo.Enabled = true;
                dirty = true;
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
        public static byte[][] Clone(byte[][] data)
        {
            int len = data.GetLength(0);
            byte[][] newData = new byte[len][];
            for (int l = 0; l < len; l++)
                newData[l] = data[l].Clone() as byte[];
            return newData;
        }
    }

    public class Action
    {
        public LevelEditorControl EdControl;
        public NSMBLevel level;

        public Action() { }
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
        }
        public virtual void Undo() { }
        public virtual void Redo() { }
        public virtual void AfterAction() { }
        public virtual void AfterSetEdControl() { }
        public virtual bool CanMerge {
            get {
                return false;
            }
        }
        public virtual void Merge(Action act) { }
        public void SetEdControl(LevelEditorControl EdControl)
        {
            this.EdControl = EdControl;
            this.level = EdControl.Level;
            this.AfterSetEdControl();
        }
    }
    #region Object Actions
    public class ObjectAction : Action
    {
        public NSMBObject obj;
        public ObjectAction(NSMBObject obj)
        {
            this.obj = obj;
        }
        public override void AfterAction()
        {
            if (level.Objects.Contains(obj))
                EdControl.SelectObject(obj);
            else
                EdControl.SelectObject(null);
        }
    }

    public class AddObjectAction : ObjectAction
    {
        public AddObjectAction(NSMBObject obj)
            : base(obj) { }
        public override void Undo()
        {
            level.Objects.Remove(obj);
        }
        public override void Redo()
        {
            level.Objects.Add(obj);
        }
        public override string ToString()
        {
            return LanguageManager.GetList("UndoActions")[0];
        }
    }
    public class RemoveObjectAction : ObjectAction
    {
        public RemoveObjectAction(NSMBObject obj)
            : base(obj) { }
        public override void Undo()
        {
            level.Objects.Add(obj);
        }
        public override void Redo()
        {
            level.Objects.Remove(obj);
        }
        public override string ToString()
        {
            return LanguageManager.GetList("UndoActions")[1];
        }
    }
    public class SizeObjectAction : ObjectAction
    {
        int OrigWidth, OrigHeight, NewWidth, NewHeight;
        public SizeObjectAction(NSMBObject obj, int NewWidth, int NewHeight)
            : base(obj)
        {
            this.OrigWidth = obj.Width;
            this.OrigHeight = obj.Height;
            this.NewWidth = Math.Max(1, NewWidth);
            this.NewHeight = Math.Max(1, NewHeight);
        }
        public override void Undo()
        {
            obj.Width = OrigWidth;
            obj.Height = OrigHeight;
            obj.UpdateObjCache();
        }
        public override void Redo()
        {
            obj.Width = NewWidth;
            obj.Height = NewHeight;
            obj.UpdateObjCache();
        }
        public override bool CanMerge {
            get {
                return true;
            }
        }
        public override void Merge(Action act)
        {
            SizeObjectAction soa = act as SizeObjectAction;
            this.NewWidth = soa.NewWidth;
            this.NewHeight = soa.NewHeight;
        }
        public override string ToString()
        {
            return LanguageManager.GetList("UndoActions")[2];
        }
    }
    public class MoveObjectAction : ObjectAction
    {
        int OrigX, OrigY, NewX, NewY;
        public MoveObjectAction(NSMBObject obj, int NewX, int NewY)
            : base(obj)
        {
            this.OrigX = obj.X;
            this.OrigY = obj.Y;
            this.NewX = NewX;
            this.NewY = NewY;
        }
        public override void Undo()
        {
            obj.X = OrigX;
            obj.Y = OrigY;
        }
        public override void Redo()
        {
            obj.X = NewX;
            obj.Y = NewY;
        }
        public override bool CanMerge {
            get {
                return true;
            }
        }
        public override void Merge(Action act)
        {
            MoveObjectAction moa = act as MoveObjectAction;
            this.NewX = moa.NewX;
            this.NewY = moa.NewY;
        }
        public override string ToString()
        {
            return LanguageManager.GetList("UndoActions")[3];
        }
    }

    public class ChangeObjectTypeAction : ObjectAction
    {
        int OrigTS, OrigNum, NewTS, NewNum;
        public ChangeObjectTypeAction(NSMBObject obj, int NewTS, int NewNum)
            : base(obj)
        {
            this.OrigTS = obj.Tileset;
            this.OrigNum = obj.ObjNum;
            this.NewTS = NewTS;
            this.NewNum = NewNum;
        }
        public override void Undo()
        {
            obj.Tileset = OrigTS;
            obj.ObjNum = OrigNum;
            obj.UpdateObjCache();
        }
        public override void Redo()
        {
            obj.Tileset = NewTS;
            obj.ObjNum = NewNum;
            obj.UpdateObjCache();
        }
        public override bool CanMerge {
            get {
                return true;
            }
        }
        public override void Merge(Action act)
        {
            ChangeObjectTypeAction cota = act as ChangeObjectTypeAction;
            this.NewTS = cota.NewTS;
            this.NewNum = cota.NewNum;
        }
        public override string ToString()
        {
            return LanguageManager.GetList("UndoActions")[4];
        }
    }
    #endregion
    #region Sprite Actions
    public class SpriteAction : Action
    {
        public NSMBSprite sprite;
        public bool UpdateDataEditor = false;
        public SpriteAction(NSMBSprite sprite)
        {
            this.sprite = sprite;
        }
        public override void AfterAction()
        {
            if (level.Sprites.Contains(sprite)) {
                if (EdControl.mode is ObjectsEditionMode)
                    (EdControl.mode as ObjectsEditionMode).RefreshDataEd = !UpdateDataEditor;
                EdControl.SelectObject(sprite);
            } else
                EdControl.SelectObject(null);
        }
    }
    public class AddSpriteAction : SpriteAction
    {
        public AddSpriteAction(NSMBSprite sprite)
            : base(sprite) { }
        public override void Undo()
        {
            level.Sprites.Remove(sprite);
        }
        public override void Redo()
        {
            level.Sprites.Add(sprite);
        }
        public override string ToString()
        {
            return LanguageManager.GetList("UndoActions")[5];
        }
    }
    public class RemoveSpriteAction : SpriteAction
    {
        public RemoveSpriteAction(NSMBSprite sprite)
            : base(sprite) { }
        public override void Undo()
        {
            level.Sprites.Add(sprite);
        }
        public override void Redo()
        {
            level.Sprites.Remove(sprite);
        }
        public override string ToString()
        {
            return LanguageManager.GetList("UndoActions")[6];
        }
    }
    public class MoveSpriteAction : SpriteAction
    {
        int OrigX, OrigY, NewX, NewY;
        public MoveSpriteAction(NSMBSprite sprite, int NewX, int NewY)
            : base(sprite)
        {
            this.OrigX = sprite.X;
            this.OrigY = sprite.Y;
            this.NewX = NewX;
            this.NewY = NewY;
        }
        public override void Undo()
        {
            sprite.X = OrigX;
            sprite.Y = OrigY;
        }
        public override void Redo()
        {
            sprite.X = NewX;
            sprite.Y = NewY;
        }
        public override bool CanMerge {
            get {
                return true;
            }
        }
        public override void Merge(Action act)
        {
            MoveSpriteAction msa = act as MoveSpriteAction;
            this.NewX = msa.NewX;
            this.NewY = msa.NewY;
        }
        public override string ToString()
        {
            return LanguageManager.GetList("UndoActions")[7];
        }
    }
    public class ChangeSpriteTypeAction : SpriteAction
    {
        int OrigType, NewType;
        public ChangeSpriteTypeAction(NSMBSprite sprite, int NewType)
            : base(sprite)
        {
            this.OrigType = sprite.Type;
            this.NewType = NewType;
            this.UpdateDataEditor = true;
        }
        public override void Undo()
        {
            sprite.Type = OrigType;
        }
        public override void Redo()
        {
            sprite.Type = NewType;
        }
        public override bool CanMerge {
            get {
                return true;
            }
        }
        public override void Merge(Action act)
        {
            ChangeSpriteTypeAction csta = act as ChangeSpriteTypeAction;
            this.NewType = csta.NewType;
        }
        public override string ToString()
        {
            return LanguageManager.GetList("UndoActions")[8];
        }
    }
    public class ChangeSpriteDataAction : SpriteAction
    {
        byte[] OrigData, NewData;
        public ChangeSpriteDataAction(NSMBSprite sprite, byte[] NewData) 
            : base(sprite)
        {
            this.OrigData = sprite.Data.Clone() as byte[];
            this.NewData = NewData;
        }
        public override void Undo()
        {
            sprite.Data = OrigData.Clone() as byte[];
        }
        public override void Redo()
        {
            sprite.Data = NewData.Clone() as byte[];
        }
        public override bool CanMerge {
            get {
                return true;
            }
        }
        public override void Merge(Action act)
        {
            ChangeSpriteDataAction csda = act as ChangeSpriteDataAction;
            this.NewData = csda.NewData;
        }
        public override string ToString()
        {
            return LanguageManager.GetList("UndoActions")[9];
        }
    }
    #endregion
    #region Multiple Actions
    public class MultipleAction : Action
    {
        public object[] objs;
        public MultipleAction(object[] objs)
        {
            this.objs = objs;
        }
        public override void AfterAction()
        {
            if (objs.Length == 0)
                return;
            if (EdControl.mode is ObjectsEditionMode && objs != null && (EdControl.Level.Objects.Contains(objs[0] as NSMBObject) || EdControl.Level.Sprites.Contains(objs[0] as NSMBSprite)))
                (EdControl.mode as ObjectsEditionMode).SelectObjects(objs);
            else
                EdControl.SelectObject(null);
        }
    }
    public class AddMultipleAction : MultipleAction
    {
        public AddMultipleAction(object[] objs)
            : base(objs) { }
        public override void Undo()
        {
            foreach (object obj in objs) {
                if (obj is NSMBObject)
                    level.Objects.Remove(obj as NSMBObject);
                if (obj is NSMBSprite)
                    level.Sprites.Remove(obj as NSMBSprite);
            }
        }
        public override void Redo()
        {
            foreach (object obj in objs) {
                if (obj is NSMBObject)
                    level.Objects.Add(obj as NSMBObject);
                if (obj is NSMBSprite)
                    level.Sprites.Add(obj as NSMBSprite);
            }
        }
        public override string ToString()
        {
            return string.Format(LanguageManager.GetList("UndoActions")[10], objs.Length);
        }
    }
    public class RemoveMultipleAction : MultipleAction
    {
        public int[] zindex;
        public RemoveMultipleAction(object[] objs)
            : base(objs)
        {
        }
        public override void Undo()
        {
            for (int l = 0; l < objs.Length; l++) {
                if (objs[l] is NSMBObject) {
                    if (level.Objects.Count - 1 > zindex[l])
                        level.Objects.Add(objs[l] as NSMBObject);
                    else
                        level.Objects.Insert(zindex[l], objs[l] as NSMBObject);
                }
                if (objs[l] is NSMBSprite) {
                    if (level.Sprites.Count - 1 > zindex[l])
                        level.Sprites.Add(objs[l] as NSMBSprite);
                    else
                        level.Sprites.Insert(zindex[l], objs[l] as NSMBSprite);
                }
            }
        }
        public override void Redo()
        {
            foreach (object obj in objs) {
                if (obj is NSMBObject)
                    level.Objects.Remove(obj as NSMBObject);
                if (obj is NSMBSprite)
                    level.Sprites.Remove(obj as NSMBSprite);
            }
        }
        public override void AfterSetEdControl()
        {
            zindex = new int[objs.Length];
            for (int l = 0; l < objs.Length; l++)
            {
                if (objs[l] is NSMBObject)
                    zindex[l] = level.Objects.IndexOf(objs[l] as NSMBObject);
                if (objs[l] is NSMBSprite)
                    zindex[l] = level.Sprites.IndexOf(objs[l] as NSMBSprite);
            }
        }
        public override string ToString()
        {
            return string.Format(LanguageManager.GetList("UndoActions")[11], objs.Length);
        }
    }
    public class MoveMultipleAction : MultipleAction
    {
        int XDelta, YDelta;
        public MoveMultipleAction(object[] objs, int XDelta, int YDelta)
            : base(objs)
        {
            this.XDelta = XDelta;
            this.YDelta = YDelta;
        }
        public MoveMultipleAction(object[] objs, object BaseObj, int NewX, int NewY)
            : base(objs)
        {
            if (BaseObj is NSMBObject) {
                this.XDelta = NewX - (BaseObj as NSMBObject).X;
                this.YDelta = NewY - (BaseObj as NSMBObject).Y;
            }
            if (BaseObj is NSMBSprite) {
                this.XDelta = NewX - (BaseObj as NSMBSprite).X;
                this.YDelta = NewY - (BaseObj as NSMBSprite).Y;
            }
        }
        public override void Undo()
        {
            foreach (object obj in objs) {
                if (obj is NSMBObject) {
                    NSMBObject o = obj as NSMBObject;
                    o.X -= XDelta;
                    o.Y -= YDelta;
                }
                if (obj is NSMBSprite) {
                    NSMBSprite s = obj as NSMBSprite;
                    s.X -= XDelta;
                    s.Y -= YDelta;
                }
            }
        }
        public override void Redo()
        {
            XDelta = -XDelta;
            YDelta = -YDelta;
            Undo();
            XDelta = -XDelta;
            YDelta = -YDelta;
        }
        public override bool CanMerge {
            get {
                return true;
            }
        }
        public override void Merge(Action act)
        {
            MoveMultipleAction mma = act as MoveMultipleAction;
            this.XDelta += mma.XDelta;
            this.YDelta += mma.YDelta;
        }
        public override string ToString()
        {
            return string.Format(LanguageManager.GetList("UndoActions")[12], objs.Length);
        }
    }
    #endregion
    #region Entrance Actions
    public class EntranceAction : Action
    {
        public NSMBEntrance en;
        public EntranceAction(NSMBEntrance en)
        {
            this.en = en;
        }
        public override void AfterAction()
        {
            if (level.Entrances.Contains(en))
                EdControl.SelectObject(en);
            else
                EdControl.SelectObject(null);
        }
    }
    public class AddEntranceAction : EntranceAction
    {
        public AddEntranceAction(NSMBEntrance en)
            : base(en) { }
        public override void Undo()
        {
            level.Entrances.Remove(en);
        }
        public override void Redo()
        {
            level.Entrances.Add(en);
        }
        public override string ToString()
        {
            return LanguageManager.GetList("UndoActions")[13];
        }
    }
    public class RemoveEntranceAction : EntranceAction
    {
        public RemoveEntranceAction(NSMBEntrance en)
            : base(en) { }
        public override void Undo()
        {
            level.Entrances.Add(en);
        }
        public override void Redo()
        {
            level.Entrances.Remove(en);
        }
        public override string ToString()
        {
            return LanguageManager.GetList("UndoActions")[14];
        }
    }
    public class MoveEntranceAction : EntranceAction
    {
        int OrigX, OrigY, NewX, NewY;
        public MoveEntranceAction(NSMBEntrance en, int NewX, int NewY)
            : base(en)
        {
            OrigX = en.X;
            OrigY = en.Y;
            this.NewX = NewX;
            this.NewY = NewY;
        }
        public override void Undo()
        {
            en.X = OrigX;
            en.Y = OrigY;
        }
        public override void Redo()
        {
            en.X = NewX;
            en.Y = NewY;
        }
        public override bool CanMerge {
            get {
                return true;
            }
        }
        public override void Merge(Action act)
        {
            MoveEntranceAction mea = act as MoveEntranceAction;
            this.NewX = mea.NewX;
            this.NewY = mea.NewY;
        }
        public override string ToString()
        {
            return LanguageManager.GetList("UndoActions")[15];
        }
    }
    public class ChangeEntranceDataAction : EntranceAction
    {
        int PropNum, OrigV, NewV;
        public ChangeEntranceDataAction(NSMBEntrance en, int PropNum, int NewV)
            : base(en)
        {
            switch (Math.Min(PropNum, 8)) {
                case 0: OrigV = en.CameraX; break;
                case 1: OrigV = en.CameraY; break;
                case 2: OrigV = en.Number; break;
                case 3: OrigV = en.DestArea; break;
                case 4: OrigV = en.DestEntrance; break;
                case 5: OrigV = en.ConnectedPipeID; break;
                case 6: OrigV = en.EntryView; break;
                case 7: OrigV = en.Type; break;
                case 8: OrigV = en.Settings; break;
            }
            this.NewV = NewV;
            this.PropNum = PropNum;
        }
        public override void Undo()
        {
            Write(OrigV);
        }
        public override void Redo()
        {
            Write(NewV);
        }
        private void Write(int value)
        {
            switch (Math.Min(PropNum, 8)) {
                case 0: en.CameraX = value; break;
                case 1: en.CameraY = value; break;
                case 2: en.Number = value; break;
                case 3: en.DestArea = value; break;
                case 4: en.DestEntrance = value; break;
                case 5: en.ConnectedPipeID = value; break;
                case 6: en.EntryView = value; break;
                case 7: en.Type = value; break;
                case 8: en.Settings = value; break;
            }
        }
        public override string ToString()
        {
            return string.Format(LanguageManager.GetList("UndoActions")[16], LanguageManager.Get("EntranceEditor", PropNum + 5));
        }
    }
    #endregion
    #region Path Actions
    public class PathAction : Action
    {
        public NSMBPath p;
        public NSMBPathPoint pn;
        
        public PathAction(NSMBPath p)
        {
            this.p = p;
            
        }
        public PathAction(NSMBPathPoint pn)
        {
            this.p = pn.parent;
            this.pn = pn;
        }
        public override void AfterAction()
        {
            if (pn != null)
                EdControl.SelectObject(pn);
            else if (pn == null)
                EdControl.SelectObject(p.points[0]);
            else
                EdControl.SelectObject(null);
        }
    }
    public class AddPathAction : PathAction
    {
        public AddPathAction(NSMBPath p)
            : base(p) { }
        public override void Undo()
        {
            if (p.isProgressPath)
                level.ProgressPaths.Remove(p);
            else
                level.Paths.Remove(p);
        }
        public override void Redo()
        {
            if (p.isProgressPath)
                level.ProgressPaths.Add(p);
            else
                level.Paths.Add(p);
        }

        public override string ToString()
        {
            return LanguageManager.GetList("UndoActions")[17];
        }
    }
    public class RemovePathAction : PathAction
    {
        public RemovePathAction(NSMBPath p)
            : base(p) { }
        public override void Undo()
        {
            if (p.isProgressPath)
                level.ProgressPaths.Add(p);
            else
                level.Paths.Add(p);
        }

        public override void Redo()
        {
            if (p.isProgressPath)
                level.ProgressPaths.Remove(p);
            else
                level.Paths.Remove(p);
        }

        public override string ToString()
        {
            return LanguageManager.GetList("UndoActions")[18];
        }
    }
    public class MovePathAction : PathAction
    {
        int XDelta, YDelta;
        public MovePathAction(NSMBPathPoint pn, int NewX, int NewY)
            : base(pn)
        {
            this.XDelta = NewX - pn.X;
            this.YDelta = NewY - pn.Y;
        }
        public override void Undo()
        {
            foreach (NSMBPathPoint n in p.points) {
                n.X -= XDelta;
                n.Y -= YDelta;
            }
        }
        public override void Redo()
        {
            foreach (NSMBPathPoint n in p.points) {
                n.X += XDelta;
                n.Y += YDelta;
            }
        }
        public override bool CanMerge {
            get {
                return true;
            }
        }
        public override void Merge(Action act)
        {
            MovePathAction mpa = act as MovePathAction;
            this.XDelta = mpa.XDelta;
            this.YDelta = mpa.YDelta;
        }
        public override string ToString()
        {
            return LanguageManager.GetList("UndoActions")[19];
        }
    }
    public class ChangePathIDAction : PathAction
    {
        int OrigID, NewID;
        public ChangePathIDAction(NSMBPath p, int NewID)
            : base(p)
        {
            OrigID = p.id;
            this.NewID = NewID;
        }
        public override void Undo()
        {
            p.id = OrigID;
        }
        public override void Redo()
        {
            p.id = NewID;
        }
        public override string ToString()
        {
            return LanguageManager.GetList("UndoActions")[20];
        }
    }
    public class AddPathNodeAction : PathAction
    {
        int index;
        public AddPathNodeAction(NSMBPathPoint pn, int index)
            : base(pn)
        {
            this.index = index;
        }
        public override void Undo()
        {
            p.points.Remove(pn);
        }
        public override void Redo()
        {
            p.points.Insert(index, pn);
        }
        public override string ToString()
        {
            return LanguageManager.GetList("UndoActions")[21];
        }
    }
    public class RemovePathNodeAction : PathAction
    {
        int index;
        public RemovePathNodeAction(NSMBPathPoint pn)
            : base(pn)
        {
            index = p.points.IndexOf(pn);
        }
        public override void Undo()
        {
            p.points.Insert(index, pn);
        }
        public override void Redo()
        {
            p.points.Remove(pn);
        }
        public override string ToString()
        {
            return LanguageManager.GetList("UndoActions")[22];
        }
    }
    public class MovePathNodeAction : PathAction
    {
        int OrigX, OrigY, NewX, NewY;
        public MovePathNodeAction(NSMBPathPoint pn, int NewX, int NewY)
            : base(pn)
        {
            this.OrigX = pn.X;
            this.OrigY = pn.Y;
            this.NewX = NewX;
            this.NewY = NewY;
        }
        public override void Undo()
        {
            pn.X = OrigX;
            pn.Y = OrigY;
        }
        public override void Redo()
        {
            pn.X = NewX;
            pn.Y = NewY;
        }
        public override bool CanMerge {
            get {
                return true;
            }
        }
        public override void Merge(Action act)
        {
            MovePathNodeAction mpna = act as MovePathNodeAction;
            this.NewX = mpna.NewX;
            this.NewY = mpna.NewY;
        }
        public override string ToString()
        {
            return LanguageManager.GetList("UndoActions")[23];
        }
    }
    public class ChangePathNodeData : PathAction
    {
        int PropNum, OrigV, NewV;
        public ChangePathNodeData(NSMBPathPoint pn, int PropNum, int NewV)
            : base(pn)
        {
            switch (PropNum)
            {
                case 0: OrigV = pn.Unknown1; break;
                case 1: OrigV = pn.Unknown2; break;
                case 2: OrigV = pn.Unknown3; break;
                case 3: OrigV = pn.Unknown4; break;
                case 4: OrigV = pn.Unknown5; break;
                case 5: OrigV = pn.Unknown6; break;
            }
            this.PropNum = PropNum;
            this.NewV = NewV;
        }
        public override void Undo()
        {
            Set((ushort)OrigV);
        }
        public override void Redo()
        {
            Set((ushort)NewV);
        }
        private void Set(ushort value)
        {
            switch (PropNum)
            {
                case 0: pn.Unknown1 = value; break;
                case 1: pn.Unknown2 = value; break;
                case 2: pn.Unknown3 = value; break;
                case 3: pn.Unknown4 = value; break;
                case 4: pn.Unknown5 = value; break;
                case 5: pn.Unknown6 = value; break;
            }
        }
        public override string ToString()
        {
            return string.Format(LanguageManager.GetList("UndoActions")[24], LanguageManager.Get("PathEditor", PropNum + 7));
        }
    }
    #endregion
    #region View and Zone Actions
    public class ViewAction : Action
    {
        public NSMBView view;
        public int ZoneIndexOffset = 0;
        public List<NSMBView> LevelList;
        public ViewAction(NSMBView view)
        {
            this.view = view;
        }
        public override void AfterSetEdControl()
        {
            if (view.isZone) {
                ZoneIndexOffset = 5;
                LevelList = level.Zones;
            } else
                LevelList = level.Views;
        }
        public override void AfterAction()
        {
            if (LevelList.Contains(view))
                EdControl.SelectObject(view);
            else
                EdControl.SelectObject(null);
        }
    }
    public class AddViewAction : ViewAction
    {
        public AddViewAction(NSMBView view)
            : base(view) { }
        public override void Undo()
        {
            LevelList.Remove(view);
        }
        public override void Redo()
        {
            LevelList.Add(view);
        }
        public override string ToString()
        {
            return LanguageManager.GetList("UndoActions")[25 + ZoneIndexOffset];
        }
    }
    public class RemoveViewAction : ViewAction
    {
        public RemoveViewAction(NSMBView view)
            : base(view) { }
        public override void Undo()
        {
            LevelList.Add(view);
        }
        public override void Redo()
        {
            LevelList.Remove(view);
        }
        public override string ToString()
        {
            return LanguageManager.GetList("UndoActions")[26 + ZoneIndexOffset];
        }
    }
    public class SizeViewAction : ViewAction
    {
        int OrigWidth, OrigHeight, NewWidth, NewHeight;
        public SizeViewAction(NSMBView view, int NewWidth, int NewHeight)
            : base(view)
        {
            this.OrigWidth = view.Width;
            this.OrigHeight = view.Height;
            this.NewWidth = NewWidth;
            this.NewHeight = NewHeight;
        }
        public override void Undo()
        {
            view.Width = OrigWidth;
            view.Height = OrigHeight;
        }
        public override void Redo()
        {
            view.Width = NewWidth;
            view.Height = NewHeight;
        }
        public override bool CanMerge {
            get {
                return true;
            }
        }
        public override void Merge(Action act)
        {
            SizeViewAction sva = act as SizeViewAction;
            this.NewWidth = sva.NewWidth;
            this.NewHeight = sva.NewHeight;
        }
        public override string ToString()
        {
            return LanguageManager.GetList("UndoActions")[27 + ZoneIndexOffset];
        }
    }
    public class MoveViewAction : ViewAction
    {
        int OrigX, OrigY, NewX, NewY;
        public MoveViewAction(NSMBView view, int NewX, int NewY)
            : base(view)
        {
            this.OrigX = view.X;
            this.OrigY = view.Y;
            this.NewX = Math.Max(0, NewX);
            this.NewY = Math.Max(0, NewY);
        }
        public override void Undo()
        {
            view.X = OrigX;
            view.Y = OrigY;
        }
        public override void Redo()
        {
            view.X = NewX;
            view.Y = NewY;
        }
        public override bool CanMerge {
            get {
                return true;
            }
        }
        public override void Merge(Action act)
        {
            MoveViewAction mva = act as MoveViewAction;
            this.NewX = mva.NewX;
            this.NewY = mva.NewY;
        }
        public override string ToString()
        {
            return LanguageManager.GetList("UndoActions")[28 + ZoneIndexOffset];
        }
    }
    public class ChangeViewDataAction : ViewAction
    {
        int PropNum, OrigV, NewV;
        public ChangeViewDataAction(NSMBView view, int PropNum, int NewV)
            : base(view)
        {
            switch (PropNum)
            {
                case 0: OrigV = view.Number; break;
                case 1: OrigV = view.Camera; break;
                case 2: OrigV = view.Music; break;
                case 3: OrigV = view.Unknown1; break;
                case 4: OrigV = view.Unknown2; break;
                case 5: OrigV = view.Unknown3; break;
                case 6: OrigV = view.Lighting; break;
                case 7: OrigV = view.FlagpoleID; break;
            }
            this.PropNum = PropNum;
            this.NewV = NewV;
        }
        public override void Undo()
        {
            Set(OrigV);
        }
        public override void Redo()
        {
            Set(NewV);
        }
        private void Set(int value)
        {
            switch (PropNum)
            {
                case 0: view.Number = value; break;
                case 1: view.Camera = value; break;
                case 2: view.Music = value; break;
                case 3: view.Unknown1 = value; break;
                case 4: view.Unknown2 = value; break;
                case 5: view.Unknown3 = value; break;
                case 6: view.Lighting = value; break;
                case 7: view.FlagpoleID = value; break;
            }
        }
        public override string ToString()
        {
            if (view.isZone)
                return LanguageManager.GetList("UndoActions")[34];
            else
                return string.Format(LanguageManager.GetList("UndoActions")[29], LanguageManager.Get("ViewEditor", PropNum + 7));
        }
    }
    #endregion
    #region Other
    public class ReplaceSpritesAction : Action
    {
        int type, newType;
        List<NSMBSprite> AffectedSprites;
        public ReplaceSpritesAction(int type, int newType)
        {
            this.type = type;
            this.newType = newType;
        }
        public override void Undo()
        {
            foreach (NSMBSprite s in AffectedSprites)
                s.Type = type;
        }
        public override void Redo()
        {
            foreach (NSMBSprite s in AffectedSprites)
                s.Type = newType;
        }
        public override void AfterSetEdControl()
        {
            AffectedSprites = new List<NSMBSprite>();
            foreach (NSMBSprite s in EdControl.Level.Sprites)
                if (s.Type == type)
                    AffectedSprites.Add(s);
        }
        public override void AfterAction()
        {
            if (EdControl.mode is ObjectsEditionMode && AffectedSprites != null)
                (EdControl.mode as ObjectsEditionMode).SelectObjects(AffectedSprites.ToArray());
            else
                EdControl.SelectObject(null);
        }
        public override string ToString()
        {
            return LanguageManager.GetList("UndoActions")[35];
        }
    }
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
        }
        public override void Redo()
        {
            EdControl.Level.Blocks = UndoManager.Clone(newData);
        }
        public override void AfterSetEdControl()
        {
            this.oldData = UndoManager.Clone(EdControl.Level.Blocks);
        }
        public override void AfterAction()
        {
            EdControl.Level.CalculateSpriteModifiers();
        }
        public override string ToString()
        {
            return LanguageManager.GetList("UndoActions")[36];
        }
    }
    #endregion
}
