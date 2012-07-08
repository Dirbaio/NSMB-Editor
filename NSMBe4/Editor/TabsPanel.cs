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

﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace NSMBe4
{
    public partial class TabsPanel : UserControl
    {
        public ImageList images;
        public LevelEditorControl EdControl;
        public List<LevelItem> SelectedObjs;

        public CreatePanel create;
        public ObjectEditor objects;
        public SpriteEditor sprites;
        public EntranceEditor entrances;
        public ViewEditor views;
        public ViewEditor zones;
        public PathEditor paths;
        public PathEditor progresspaths;

        public Control[] controls;

        public List<Control> activeCtrls = new List<Control>();

        public TabsPanel(LevelEditorControl EdControl) {
            InitializeComponent();
            this.EdControl = EdControl;

            images = new ImageList();
            images.ColorDepth = ColorDepth.Depth32Bit;
            images.Images.Add(Properties.Resources.add);
            images.Images.Add(Properties.Resources.block);
            images.Images.Add(Properties.Resources.bug);
            images.Images.Add(Properties.Resources.door);
            images.Images.Add(Properties.Resources.views);
            images.Images.Add(Properties.Resources.zones);
            images.Images.Add(Properties.Resources.paths);
            images.Images.Add(Properties.Resources.paths_progress);
            tabControl1.ImageList = images;

            create = new CreatePanel(EdControl);
            objects = new ObjectEditor(EdControl);
            sprites = new SpriteEditor(EdControl);
            entrances = new EntranceEditor(EdControl);
            views = new ViewEditor(EdControl, EdControl.Level.Views, true);
            zones = new ViewEditor(EdControl, EdControl.Level.Zones, false);
            paths = new PathEditor(EdControl, EdControl.Level.Paths);
            progresspaths = new PathEditor(EdControl, EdControl.Level.ProgressPaths);
            controls = new Control[] {create, objects, sprites, entrances, views, zones, paths, progresspaths};

            SelectNone();
        }

        public void SelectObjects(List<LevelItem> objs, LevelItem selectedTabType)
        {
            if (objs.Contains(selectedTabType))
            {
                objs.Remove(selectedTabType);
                objs.Insert(0, selectedTabType);
            }
            SelectedObjs = objs;
            ClearTabs();
            int tabIndex = -1;
            foreach (LevelItem obj in objs) {
                if (obj is NSMBObject) AddTab(objects);
                if (obj is NSMBSprite) AddTab(sprites);
                if (obj is NSMBEntrance) AddTab(entrances);
                if (obj is NSMBView) {
                    NSMBView v = obj as NSMBView;
                    if (v.isZone)
                        AddTab(zones);
                    else
                        AddTab(views);
                }
                if (obj is NSMBPathPoint) {
                    NSMBPathPoint pp = obj as NSMBPathPoint;
                    if (pp.parent.isProgressPath)
                        AddTab(progresspaths);
                    else
                        AddTab(paths);
                }

                if (selectedTabType != null && obj.GetType() == selectedTabType.GetType() && tabIndex == -1)
                    tabIndex = tabControl1.TabCount - 1;
            }
            if (tabControl1.TabCount == 0)
                SelectNone();
            if (tabIndex > -1)
                tabControl1.SelectedIndex = tabIndex;
            EdControl.Focus();
        }

        public void SelectNone()
        {
            ClearTabs();
            AddTab(create);
            AddTab(entrances);
            AddTab(views);
            AddTab(zones);
            AddTab(paths);
            AddTab(progresspaths);

            entrances.SelectObjects(null);
            views.SelectObjects(null);
            zones.SelectObjects(null);
            paths.SelectObjects(null);
            progresspaths.SelectObjects(null);
            EdControl.Focus();
        }

        public void ClearTabs()
        {
            activeCtrls.Clear();
            tabControl1.TabPages.Clear();
        }

        public void AddTab(Control ctrl)
        {
            if (!activeCtrls.Contains(ctrl)) {
                if (ctrl is ObjectEditor) (ctrl as ObjectEditor).SelectObjects(SelectedObjs);
                if (ctrl is EntranceEditor) (ctrl as EntranceEditor).SelectObjects(SelectedObjs);
                if (ctrl is SpriteEditor) (ctrl as SpriteEditor).SelectObjects(SelectedObjs);
                if (ctrl is ViewEditor) (ctrl as ViewEditor).SelectObjects(SelectedObjs);
                if (ctrl is PathEditor) (ctrl as PathEditor).SelectObjects(SelectedObjs);

                TabPage tp = new TabPage("");
                tp.ImageIndex = Array.IndexOf(controls, ctrl);
                tp.Controls.Add(ctrl);
                ctrl.Dock = DockStyle.Fill;
                tabControl1.TabPages.Add(tp);
                activeCtrls.Add(ctrl);
            }
        }

        public void RefreshTabs()
        {
            foreach (Control ctrl in activeCtrls)
            {
                if (ctrl is ObjectEditor) (ctrl as ObjectEditor).UpdateInfo();
                if (ctrl is EntranceEditor) (ctrl as EntranceEditor).UpdateInfo();
                if (ctrl is SpriteEditor) (ctrl as SpriteEditor).UpdateInfo();
                if (ctrl is ViewEditor) (ctrl as ViewEditor).UpdateInfo();
                if (ctrl is PathEditor) (ctrl as PathEditor).UpdateInfo();
            }
        }

        public void UpdateSpriteEditor()
        {
            foreach (Control ctrl in activeCtrls)
                if (ctrl is SpriteEditor) (ctrl as SpriteEditor).UpdateDataEditor();
        }

        public void RefreshSpriteEditor()
        {
            foreach (Control ctrl in activeCtrls)
                if (ctrl is SpriteEditor) (ctrl as SpriteEditor).RefreshDataEditor();
        }
    }
}
