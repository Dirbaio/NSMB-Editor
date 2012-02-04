using System;
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

        public CreatePanel create;
        public ObjectEditor objects;
        public SpriteEditor sprites;
        public EntranceEditor entrances;
        public ViewEditor views;
        public ViewEditor zones;
        public PathEditor paths;
        public PathEditor progresspaths;
        public LevelConfig config;

        public Control[] controls;

        enum ItemType
        {
            Object = 2,
            Sprite = 3,
            Entrance = 4,
            View = 5,
            Zone = 6,
            Path = 7,
            ProgressPath = 8
        }

        public GoodTabsPanel(LevelEditorControl EdControl) {
            InitializeComponent();
            this.EdControl = EdControl;

            images = new ImageList();
            images.ColorDepth = ColorDepth.Depth32Bit;
            images.Images.Add(Properties.Resources.config);
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

            config = new LevelConfig(EdControl);

            controls = new Control[] { config, create, objects, sprites, entrances, views, zones, paths, progresspaths };

            foreach (Control c in controls)
                AddTab(c);

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

            //Wow I got the same as the shit below, but only in 5 lines!
            bool[] has = new bool[9];
            foreach (LevelItem it in SelectedObjs)
                has[(int)typeOfItem(it)] = true;

            if (SelectedObjs.Count == 0)
                tabControl1.SelectedIndex = 1;
            else
                if (!has[tabControl1.SelectedIndex])
                    tabControl1.SelectedIndex = Array.IndexOf(has, true);

            objects.Visible = has[(int)ItemType.Object];
            sprites.Visible = has[(int)ItemType.Sprite];

            /*
            bool hasObjects = false;
            bool hasSprites = false;
            bool hasEntrances = false;
            bool hasViews = false;
            bool hasZones = false;
            bool hasPaths = false;
            bool hasProgressPaths = false;

            foreach(LevelItem it in SelectedObjs)
            {
                hasObjects |= it is NSMBObject;
                hasSprites |= it is NSMBSprite;
                hasEntrances |= it is NSMBEntrance;
                hasViews |= it is NSMBView && !(it as NSMBView).isZone;
                hasZones |= it is NSMBView && (it as NSMBView).isZone;
                hasPaths |= it is NSMBPath && !(it as NSMBPath).isProgressPath;
                hasProgressPaths |= it is NSMBPath && (it as NSMBPath).isProgressPath;
            }

            Control selected = controls[tabControl1.SelectedIndex];
            if (hasObjects && selected == objects) return;
            if (hasSprites && selected == sprites) return;
            if (hasEntrances && selected == entrances) return;
            if (hasViews && selected == views) return;
            if (hasZones && selected == zones) return;
            if (hasPaths && selected == paths) return;
            if (hasProgressPaths && selected == progresspaths) return;

            if (hasObjects) select(objects);
            else if (hasSprites) select(sprites);
            else if (hasEntrances) select(entrances);
            else if (hasViews) select(views);
            else if (hasZones) select(zones);
            else if (hasPaths) select(paths);
            else if (hasProgressPaths) select(progresspaths);*/
        }
        /*
        private void select(Control c)
        {
            int ind = 0;
            for (int i = 0; i < controls.Length; i++)
                if (controls[i] == c) ind = i;

            tabControl1.SelectedIndex = ind;
        }*/

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
