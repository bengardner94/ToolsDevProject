using UnityEngine;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using System.Collections.Generic;

public class BehaviourTreeView : BTree
{
    public BehaviourTreeView(BTNode root = ScriptableObject.CreateInstance<BTInfRepeater>) : base(root)
    {
        
    }
}
