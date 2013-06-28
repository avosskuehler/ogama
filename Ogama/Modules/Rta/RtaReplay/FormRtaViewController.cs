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
        private NLog.Logger log = new NLog.LogFactory().GetCurrentClassLogger();

        private RtaModel rtaModel;
        private RtaCategoryTreeitemConverter converter = new RtaCategoryTreeitemConverter();
        private String currentPlayerPosition;
        private IFormRtaViewControllerListener listener;
        private RtaSettings rtaSettings;

        public FormRtaViewController(RtaSettings rtaSettings)
        {
            this.rtaSettings = rtaSettings;
            this.init(rtaSettings);
        }

        protected void init(RtaSettings rtaSettings)
        {
            this.rtaModel = new RtaModel();
            DaoFactory df = Ogama.Modules.Database.DaoFactoryWrapper.GetDaoFactory();
            this.rtaModel.Init(df);
            this.rtaModel.Load(rtaSettings);
        }

        public RtaModel getModel()
        {
            return this.rtaModel;
        }

        public void onSpaceKey()
        {
            
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
            rtaCategory.fkRtaSettings = this.rtaSettings;

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
            this.rtaModel.Save();
        }

        public void cancel()
        {
            this.rtaModel.Load(this.rtaSettings);
        }

        public void setCurrentPlayerPosition(string p)
        {
            this.currentPlayerPosition = p;
        }
    }
}
