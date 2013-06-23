using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OgamaDao.Model.Rta;
using OgamaDao.Dao;
namespace Ogama.Modules.Rta.RtaReplay
{
    public class FormRtaViewController
    {
        
        private RtaModel rtaModel;
        private RtaCategoryTreeitemConverter converter = new RtaCategoryTreeitemConverter();
        private String currentPlayerPosition;
        private IFormRtaViewControllerListener listener;


        public FormRtaViewController()
        {
            this.init();
        }

        protected void init()
        {
            this.rtaModel = new RtaModel();
            DaoFactory df = Ogama.Modules.Database.DaoFactoryWrapper.GetDaoFactory();
            this.rtaModel.SetRtaCategoryDao(df.GetRtaCategoyDao());
            this.rtaModel.SetRtaEventDao(df.getRtaEventDao());
            this.rtaModel.Load();
        }

        public RtaModel getModel()
        {
            return this.rtaModel;
        }


        public void loadCategoryList(System.Windows.Forms.TreeNodeCollection controls)
        {
            converter.LoadFlatModelIntoTreeNodeCollection(rtaModel, controls);
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
                this.rtaModel.Add(rtaCategory);
            }
            else
            {
                parent.Nodes.Add(newNode);
                RtaCategory parentRtaCategory = (RtaCategory)parent.Tag;
                this.rtaModel.Add(rtaCategory, parentRtaCategory);
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
                this.rtaModel.Remove(rtaCategory);
            }
            treeNode.Remove();
        }


        public void save()
        {
            this.rtaModel.SaveRtaCategories();
            this.rtaModel.SaveRtaEvents();
        }

        public void cancel()
        {
            this.rtaModel.Load();
        }

        public void setCurrentPlayerPosition(string p)
        {
            this.currentPlayerPosition = p;
        }
    }
}
