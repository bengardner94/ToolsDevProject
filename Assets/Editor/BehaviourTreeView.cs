using UnityEngine;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using System.Collections.Generic;

public class BehaviourTreeView : TreeView
{
    public BehaviourTreeView(TreeViewState state) : base(state)
    {
        Reload();
    }

    protected override TreeViewItem BuildRoot()
    {
        TreeViewItem root = new TreeViewItem { id = 0, depth = -1, displayName = "Root" };
        List<TreeViewItem> allItems = new List<TreeViewItem>
        {
            
        }
    }
}
