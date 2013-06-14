using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OgamaDao.Model.Rta;

namespace Ogama.Modules.Rta.RtaReplay
{
    public partial class FormRtaView : Form, IRtaCategoryVisitor
    {

        private FormRtaViewController controller = new FormRtaViewController();

        private TreeNode currentTreeNode = null;
        private List<RtaPanel> rtaPanelList = new List<RtaPanel>();

        protected System.Threading.Thread thread;
        protected Boolean runThread = false;
        protected Tools tools = new Tools();

        

        private RtaCategory getCurrentRtaCategory()
        {
            if (this.currentTreeNode == null)
            {
                return null;
            }
            
            object tag = this.currentTreeNode.Tag;
            if (!(tag is RtaCategory))
            {
                return null;
            }

            RtaCategory rtaCategory = (RtaCategory)tag;

            return rtaCategory;
        }

        private void onTreeViewMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeNode node = e.Node;
            
            this.currentTreeNode = node;

            if (node == null)
            {
                return;
            }
            Console.WriteLine("onMouseClick:" + e.Node);
            this.onTreeNodeSelected(node);    
        }


        private void onTreeNodeSelected(TreeNode node)
        {
            if (node == null)
            {
                return;
            }
            object tag = node.Tag;
            if (! (tag is RtaCategory))
            {
                return;
            }

            RtaCategory rtaCategory = (RtaCategory)tag;
            
            string name = rtaCategory.name;
            string description = rtaCategory.description;
            Boolean active = rtaCategory.show;

            this.textBoxRtaEventName.Text = name;
            this.richTextBoxDescription.Text = description;
            this.checkBox1.Checked = active;
            
        }

        public FormRtaView()
        {
            InitializeComponent();

            controller.loadCategoryList(this.treeView1.Nodes);

            this.treeView1.NodeMouseClick += new TreeNodeMouseClickEventHandler(onTreeViewMouseClick);

            loadModel();

            loadMovie();
        }

        protected void loadMovie()
        {
            //axWindowsMediaPlayer1
            this.axWindowsMediaPlayer1.URL = "c:/data/projects/demos/demo2.mp4";
            this.axWindowsMediaPlayer1.Ctlcontrols.play();



            thread = new System.Threading.Thread(this.run);
            this.runThread = true;
            thread.Start();
            
        }


       
        private void run()
        {
            
            while (runThread)
            {
                System.Threading.Thread.Sleep(250);
                
                double position = this.axWindowsMediaPlayer1.Ctlcontrols.currentPosition;
                double max = this.axWindowsMediaPlayer1.currentMedia.duration;
                if (Double.IsNaN(max) || Double.IsNaN(position))
                {
                    continue;
                }
                double positionInPercent = tools.doubleValue2Percent(position, max);
                if (Double.IsNaN(positionInPercent))
                {
                    continue;
                }

                double currentPlayerPosition = this.axWindowsMediaPlayer1.Ctlcontrols.currentPosition;

                
                if (positionInPercent < 0)
                {
                    positionInPercent = 0;
                }

                for (int i = 0; i < rtaPanelList.Count; i++)
                {
                    RtaPanel panel = rtaPanelList.ElementAt(i);
                    panel.adjustProgressTrackerPosition(positionInPercent);
                    panel.setCurrentPlayerPositionString(currentPlayerPosition);
                }

                double segmentXposition = 532;


                
            }
        }

        
        
        protected void loadModel()
        {
            this.clear();
            RtaCategoryModel model = this.controller.getModel();
            model.visit(this);
            registerRtaPanelsToEachOther();
        }

        protected void registerRtaPanelsToEachOther()
        {
            for (int i = 0; i < rtaPanelList.Count; i++)
            {
                RtaPanel panelA = rtaPanelList.ElementAt(i);
                panelA.setRtaCategoryModel(this.controller.getModel());
                
                for (int j = 0; j < rtaPanelList.Count; j++)
                {
                    if (i == j)
                    {
                        continue;
                    }

                    RtaPanel panelB = rtaPanelList.ElementAt(j);
                    panelB.setRtaCategoryModel(this.controller.getModel());
                    panelA.AddSibling(panelB);
                }
            }
        }

        protected void clear()
        {
            for (int i = 0; i < this.rtaPanelList.Count; i++)
            {
                RtaPanel panel = this.rtaPanelList.ElementAt(i);
                panel.RemoveFromParent();
            }
            this.rtaPanelList.Clear();
        }

        private void Rta_TabIndexChanged(object sender, EventArgs e)
        {
            Console.WriteLine("Rta_TabIndexChanged");
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {
            Console.WriteLine("splitContainer1_Panel2_Paint paint:");
            
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            

        }

        private void richTextBoxDescription_Leave(object sender, EventArgs e)
        {
            if (this.getCurrentRtaCategory() == null)
            {
                return;
            }
            this.getCurrentRtaCategory().description = this.richTextBoxDescription.Text;
        }

        private void textBoxRtaEventName_Leave(object sender, EventArgs e)
        {
            if (this.getCurrentRtaCategory() == null)
            {
                return;
            }
            string name = this.textBoxRtaEventName.Text;
            this.getCurrentRtaCategory().name = name;
            this.currentTreeNode.Text = name;
        }

        private void buttonAddCategory_Click(object sender, EventArgs e)
        {
            this.controller.onAddTreeNode(this.currentTreeNode, this.treeView1.Nodes);
        }

        private void buttonDeleteCategory_Click(object sender, EventArgs e)
        {
            this.controller.onDeleteTreeNode(this.currentTreeNode, this.treeView1.Nodes);
            
        }

        private void buttonSaveModel_Click(object sender, EventArgs e)
        {
            this.controller.save();
        }
        
        public void onVisit(RtaCategory rtaCategory)
        {
            if (!rtaCategory.show)
            {
                return;
            }
            int index = rtaPanelList.Count;
            RtaPanel rtaPanel = new RtaPanel(rtaCategory.name);
            rtaPanel.setRtaCategory(rtaCategory);
            rtaPanel.setLocation(20, index * 60 + 10);
            rtaPanel.AddToParent(this.splitContainer1.Panel2);
            rtaPanelList.Add(rtaPanel);
            
        }

        public void onVisit(RtaEvent rtaEvent)
        {
            rtaPanelList.ForEach(delegate (RtaPanel rtaPanel)
            {
                if(rtaPanel.belongsToRtaEvent(rtaEvent))
                {
                    rtaPanel.addRtaEvent(rtaEvent);
                }
            });
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Boolean active = this.checkBox1.Checked;
            RtaCategory item = getCurrentRtaCategory();
            if (item == null)
            {
                return;
            }
            item.SetActive(active);
        }

        private void Rta_Selected(object sender, TabControlEventArgs e)
        {
            Console.WriteLine("selected");
            if (e.TabPageIndex == 0)
            {
                this.loadModel();
            }
        }
    }
}
