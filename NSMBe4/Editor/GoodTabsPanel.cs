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
    public partial class GoodTabsPanel : UserControl
    {
        public ImageList images;
        public LevelEditorControl EdControl;
        public List<LevelItem> SelectedObjs;

        public ObjectEditor objects;
        public SpriteEditor sprites;
        public EntranceEditor entrances;
        public ViewEditor views;
        public ViewEditor zones;
        public PathEditor paths;
        public PathEditor progresspaths;
        public LevelConfig config;

        public Control[] controls;

        public List<string> ToolTips;

        public int SelectedTab {
            get {
                return tabControl1.SelectedIndex;
            }
            set {
                tabControl1.SelectedIndex = value;
            }
        }


        enum ItemType
        {
            Object = 1,
            Sprite = 2,
            Entrance = 3,
            View = 4,
            Zone = 5,
            Path = 6,
            ProgressPath = 7
        }

        public GoodTabsPanel(LevelEditorControl EdControl) {
            InitializeComponent();
            this.EdControl = EdControl;
            ToolTips = LanguageManager.GetList("TabText");

            images = new ImageList();
            images.ColorDepth = ColorDepth.Depth32Bit;
            images.Images.Add(Properties.Resources.config);
            images.Images.Add(Properties.Resources.block);
            images.Images.Add(Properties.Resources.bug);
            images.Images.Add(Properties.Resources.door);
            images.Images.Add(Properties.Resources.views);
            images.Images.Add(Properties.Resources.zones);
            images.Images.Add(Properties.Resources.paths);
            images.Images.Add(Properties.Resources.paths_progress);
            tabControl1.ImageList = images;

            objects = new ObjectEditor(EdControl);
            sprites = new SpriteEditor(EdControl);
            entrances = new EntranceEditor(EdControl);
            views = new ViewEditor(EdControl, EdControl.Level.Views, true);
            zones = new ViewEditor(EdControl, EdControl.Level.Zones, false);
            paths = new PathEditor(EdControl, EdControl.Level.Paths);
            progresspaths = new PathEditor(EdControl, EdControl.Level.ProgressPaths);

            config = new LevelConfig(EdControl);
            config.LoadSettings();
            EdControl.config = config;

            controls = new Control[] { config, objects, sprites, entrances, views, zones, paths, progresspaths };

            foreach (Control c in controls)
                AddTab(c);
            tabControl1.SelectedIndex = 1;

            //Select nothing
            SelectObjects(new List<LevelItem>());
        }

        public void AddTab(Control ctrl)
        {
            TabPage tp = new TabPage("");
            tp.ImageIndex = Array.IndexOf(controls, ctrl);
            tp.Controls.Add(ctrl);
            ctrl.Dock = DockStyle.Fill;
            tabControl1.TabPages.Add(tp);
            if (tabControl1.TabCount <= ToolTips.Count)
                tp.ToolTipText = ToolTips[tabControl1.TabCount - 1];
        }

        public void SelectObjects(List<LevelItem> SelectedObjs)
        {
            objects.SelectObjects(filter(SelectedObjs, ItemType.Object ));
            sprites.SelectObjects(filter(SelectedObjs, ItemType.Sprite ));
            entrances.SelectObjects(filter(SelectedObjs, ItemType.Entrance ));
            views.SelectObjects(filter(SelectedObjs, ItemType.View ));
            zones.SelectObjects(filter(SelectedObjs, ItemType.Zone ));
            paths.SelectObjects(filter(SelectedObjs, ItemType.Path ));
            progresspaths.SelectObjects(filter(SelectedObjs, ItemType.ProgressPath ));

            bool[] has = new bool[8];
            foreach (LevelItem it in SelectedObjs)
                has[(int)typeOfItem(it)] = true;

            int idx = Array.IndexOf(has, true);
            if (idx > -1 && !has[tabControl1.SelectedIndex])
                tabControl1.SelectedIndex = idx;
        }

        public void UpdateInfo()
        {
            objects.UpdateInfo();
            sprites.UpdateInfo();
            entrances.UpdateInfo();
            views.UpdateInfo();
            zones.UpdateInfo();
            paths.UpdateInfo();
            progresspaths.UpdateInfo();
        }

        private ItemType typeOfItem(LevelItem it)
        {
            if (it is NSMBObject) return ItemType.Object;
            if (it is NSMBSprite) return ItemType.Sprite;
            if (it is NSMBEntrance) return ItemType.Entrance;
            if (it is NSMBView && !(it as NSMBView).isZone) return ItemType.View;
            if (it is NSMBView && (it as NSMBView).isZone) return ItemType.Zone;
            if (it is NSMBPath && !(it as NSMBPath).isProgressPath) return ItemType.Path;
            if (it is NSMBPath && (it as NSMBPath).isProgressPath) return ItemType.ProgressPath;
            if (it is NSMBPathPoint && !(it as NSMBPathPoint).parent.isProgressPath) return ItemType.Path;
            if (it is NSMBPathPoint && (it as NSMBPathPoint).parent.isProgressPath) return ItemType.ProgressPath;

            throw new Exception("me dunno wat type can i has!"); //rofl
        }

        private List<LevelItem> filter(List<LevelItem> l, ItemType t)
        {
            List<LevelItem> res = new List<LevelItem>();

            foreach (LevelItem it in l)
                if (typeOfItem(it) == t)
                    res.Add(it);

            return res;
        }
    }
}
