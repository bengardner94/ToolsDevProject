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
        var root = new TreeViewItem { }
    }
}
