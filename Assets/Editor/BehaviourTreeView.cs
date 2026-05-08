using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using UnityEngine.UIElements;

[UxmlElement]
public partial class BehaviourTreeView : TreeView
{
    public BTree m_tree;
    public int id = 0;

    public List<TreeViewItemData<BehaviourTreeItem>> CreateTreeView(BTree tree)
    {
        m_tree = tree;
        List<TreeViewItemData<BehaviourTreeItem>> nodesList = new List<TreeViewItemData<BehaviourTreeItem>>();

        BehaviourTreeItem rootItem = new BehaviourTreeItem(id, "BTreeRoot", m_tree.m_Root, this);
        nodesList.Add(new TreeViewItemData<BehaviourTreeItem>(rootItem.m_ID, rootItem));
        return nodesList;

        /*foreach (BTNode nodes in m_tree.m_Nodes)
        {
            BehaviourTreeItem bTreeItem = new BehaviourTreeItem(id, nodes.name, nodes, this);
            nodesList.Add(new TreeViewItemData<BehaviourTreeItem>(bTreeItem.m_ID, bTreeItem));
            id++;
        }*/
    }

    public void CreateNode(System.Type type)
    {
        BTNode node = m_tree.CreateNode(type);
    }

    public void DeleteNode()
    {
        
    }
}
