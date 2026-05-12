using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using UnityEngine.UIElements;

[UxmlElement]
public partial class BehaviourTreeView : TreeView
{
    public BTree m_tree;
    public int id = 0;

    public BehaviourTreeItem CreateTreeView(BTree tree)
    {
        m_tree = tree;

        BehaviourTreeItem rootItem = new BehaviourTreeItem(id, "BTreeRoot", m_tree.m_Root, null);
        return rootItem;
    }
}
