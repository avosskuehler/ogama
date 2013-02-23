using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Ogama.Modules.Rta.RtaReplay
{
    public class FormRtaViewController
    {

        private RtaCategoryModel rtaCategoryModel = new RtaCategoryModel();
        private RtaCategoryTreeitemConverter converter = new RtaCategoryTreeitemConverter();
        public string xmlFilename = "c:/testRta.xml";
        private IFormRtaViewControllerListener listener;

        public RtaCategoryModel getModel()
        {
            return this.rtaCategoryModel;
        }

        public void loadCategoryList(System.Windows.Forms.TreeNodeCollection controls)
        {
            rtaCategoryModel.ReadFromXmlFile(xmlFilename);
            converter.LoadModelIntoTreeNodeCollection(rtaCategoryModel, controls);
        }


        
        public void Register(IFormRtaViewControllerListener listener)
        {
            this.listener = listener;
        }



        public void onAddTreeNode(TreeNode parent, TreeNodeCollection nodes)
        {
            RtaCategory rtaCategory = new RtaCategory();
            rtaCategory.name = "unknown";
            rtaCategory.description = "";

            TreeNode newNode = new TreeNode();
            newNode.Text = rtaCategory.name;

            newNode.Tag = rtaCategory;
            if (parent == null)
            {
                nodes.Add(newNode);
                this.rtaCategoryModel.Add(rtaCategory);
            }
            else
            {
                parent.Nodes.Add(newNode);
                RtaCategory parentRtaCategory = (RtaCategory)parent.Tag;
                this.rtaCategoryModel.Add(rtaCategory, parentRtaCategory);
            }
        }

        public void onDeleteTreeNode(TreeNode treeNode, TreeNodeCollection nodes)
        {
            if (treeNode == null)
            {
                return;
            }
            RtaCategory rtaCategory = (RtaCategory)treeNode.Tag;
            if (rtaCategory != null)
            {
                this.rtaCategoryModel.Remove(rtaCategory);
            }
            treeNode.Remove();
        }


        public void save()
        {
            this.rtaCategoryModel.WriteToXmlFile(this.xmlFilename);
        }
    }
}
