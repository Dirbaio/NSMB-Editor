using System;
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
        public PathEditor paths;

        public List<Control> activeCtrls = new List<Control>();

        public TabsPanel(LevelEditorControl EdControl) {
            InitializeComponent();
            this.EdControl = EdControl;

            images = new ImageList();
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
            //sprites = new SpriteEditor(EdControl);
            entrances = new EntranceEditor(EdControl);
            //views = new ViewEditor(EdControl);
            //paths = new PathEditor(EdControl);

            SelectNone();
        }

        public void SelectObjects(List<LevelItem> objs)
        {
            SelectedObjs = objs;
            ClearTabs();
            foreach (LevelItem obj in objs) {
                if (obj is NSMBObject) AddTab(objects);
                //if (obj is NSMBSprite) AddTab(sprites);
                if (obj is NSMBEntrance) AddTab(entrances);
                //if (obj is NSMBView) AddTab(views);
                //if (obj is NSMBPathPoint) AddTab(paths);
            }
            if (tabControl1.TabCount == 0)
                SelectNone();
        }

        public void SelectNone()
        {
            ClearTabs();
            AddTab(create);
            AddTab(entrances);
            //AddTab(views);
            //AddTab(paths);
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

                TabPage tp = new TabPage("");
                tp.Controls.Add(ctrl);
                ctrl.Dock = DockStyle.Fill;
                tabControl1.TabPages.Add(tp);
                activeCtrls.Add(ctrl);
            }
        }
    }
}
