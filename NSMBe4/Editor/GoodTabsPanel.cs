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

        public List<Control> activeCtrls = new List<Control>();

        public GoodTabsPanel(LevelEditorControl EdControl) {
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

            config = new LevelConfig(EdControl);

            controls = new Control[] { config, create, objects, sprites, entrances, views, zones, paths, progresspaths };

            AddTab(config);
            foreach (Control c in controls)
                AddTab(c);

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

        public void SelectObjects(List<LevelItem> SelectedObjs)
        {
            objects.SelectObjects(SelectedObjs);
            sprites.SelectObjects(SelectedObjs);
            entrances.SelectObjects(SelectedObjs);
            views.SelectObjects(SelectedObjs);
            zones.SelectObjects(SelectedObjs);
            paths.SelectObjects(SelectedObjs);
            progresspaths.SelectObjects(SelectedObjs);

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
            else if (hasProgressPaths) select(progresspaths);
    
        }

        private void select(Control c)
        {
            int ind = 0;
            for (int i = 0; i < controls.Length; i++)
                if (controls[i] == c) ind = i;

            tabControl1.SelectedIndex = ind;
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
