using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using UnityEngine.UIElements;
using JetBrains.Annotations;

[UxmlElement]
public partial class BehaviourTreeView : TreeView
{
    public BehaviourTreeView()
    {
        public BTree m_tree;
        public int id = 0;

        void CreateTreeView(BTree tree)
        {
            m_tree = tree;
            List<TreeViewItemData<NodeView>> nodesList = new List<TreeViewItemData<NodeView>>();

            foreach (BTNode nodes in m_tree.m_Nodes)
            {
                NodeView nodeView = new NodeView(id, nodes.name, nodes, this);
                nodesList.Add(new TreeViewItemData<NodeView>(nodeView.m_ID, nodeView));
                id++;
            }
            
        }

        public void CreateNode(System.Type type)
        {
            BTNode node = m_tree.CreateNode(type);

        }
    }
}
