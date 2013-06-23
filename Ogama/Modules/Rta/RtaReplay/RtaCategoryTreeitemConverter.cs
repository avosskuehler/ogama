using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OgamaDao.Model.Rta;
namespace Ogama.Modules.Rta.RtaReplay
{
    public class RtaCategoryTreeitemConverter
    {


        public void LoadModelIntoTreeNodeCollection(RtaModel model, System.Windows.Forms.TreeNodeCollection nodes)
        {
            List<RtaCategory> categories = new List<RtaCategory>();

            for (int i = 0; i < model.getRtaCategories().Count(); i++)
            {
                categories.Add(model.getRtaCategories().ElementAt(i));
            }

            
            Stack<TreeNode> parentNodes = new Stack<TreeNode>();

            for (int i = 0; i < categories.Count; i++)
            {
                RtaCategory item = categories.ElementAt(i);
                TreeNode node = new TreeNode(item.name);
                node.Tag = item;
                
                System.Windows.Forms.TreeNodeCollection nodeList = nodes;
                
                if (parentNodes.Count > 0)
                {
                    nodeList = parentNodes.Pop().Nodes;
                }

                nodeList.Add(node);
                

                for (int k = 0; k < item.children.Count(); k++)
                {
                    int offset = i + k + 1;
                    categories.Insert(offset, (item.children.ElementAt(k)));
                    parentNodes.Push(node);
                }
            }

            

        }



        public void LoadFlatModelIntoTreeNodeCollection(RtaModel model, System.Windows.Forms.TreeNodeCollection nodes)
        {
            Dictionary<RtaCategory, TreeNode> nodeMap = new Dictionary<RtaCategory, TreeNode>();
            foreach (RtaCategory cat in model.getRtaCategories())
            {
                
                TreeNode node = new TreeNode(cat.name);
                node.Tag = cat;

                RtaCategory parent = cat.parent;

                if (parent == null)
                {
                    nodes.Add(node);
                    nodeMap.Add(cat, node);
                }
                else
                {
                    TreeNode parentNode = new TreeNode();
                    if (nodeMap.TryGetValue(parent, out parentNode))
                    {
                        parentNode.Nodes.Add(node);
                    }
                }
            }
        }
    }
}
