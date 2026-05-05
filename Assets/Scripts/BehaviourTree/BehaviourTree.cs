using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu()]
public class BTree : ScriptableObject
{
    public BTNode m_Root;
    public List<BTNode> m_Nodes;

    public Blackboard m_Blackboard;

    public BTree(BTNode root)
    {
        m_Blackboard = new Blackboard();
        m_Root = root;
        m_Root.AddBBRecursive(m_Blackboard);
    }

    public void Process()
    {
        m_Root.Process();
    }



    public BTNode CreateNode(System.Type type)
    {
        BTNode node = ScriptableObject.CreateInstance(type) as BTNode;
        node.name = type.Name;
        m_Nodes.Add(node);

        AssetDatabase.AddObjectToAsset(node, this);
        AssetDatabase.SaveAssets();

        return node;
    }

    public void RemoveNode(BTNode node)
    {
        m_Nodes.Remove(node);
        AssetDatabase.RemoveObjectFromAsset(node);
    }
}