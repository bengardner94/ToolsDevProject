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
        m_Nodes.Add(root);
        m_Blackboard = new Blackboard();
        m_Root = root;
        m_Root.AddBBRecursive(m_Blackboard);
    }

    public void SetRoot(BTNode root)
    {
        m_Root = root;
    }
    
    public void Process()
    {
        m_Root.Process();
    }

    public void Initialize()
    {
        if (m_Nodes != null)
        {
            foreach (BTNode node in m_Nodes)
            {
                AssetDatabase.RemoveObjectFromAsset(node);
            }
        }
        m_Nodes = new List<BTNode>();
        AssetDatabase.SaveAssets();
    }

    public BTNode CreateNode(System.Type type, BTNode parent)
    {
        BTNode node = ScriptableObject.CreateInstance(type) as BTNode;
        if (parent == null)
        {
            node.name = type.Name;
            m_Nodes.Add(node);
            AssetDatabase.AddObjectToAsset(node, this);
            AssetDatabase.SaveAssets();
        }
        else
        {
            node.name = type.Name;
            m_Nodes.Add(node);
            AssetDatabase.AddObjectToAsset(node, this);
            AssetDatabase.SaveAssets();
            parent.m_Children.Add(node);
        }

        return node;
    }

    public void RemoveNode(BTNode node, BTNode parent)
    {
        m_Nodes.Remove(node);
        parent.m_Children.Remove(node);
        foreach (BTNode child in node.m_Children)
        {
            m_Nodes.Remove(child);
            AssetDatabase.RemoveObjectFromAsset(child);
        }
        AssetDatabase.RemoveObjectFromAsset(node);
        AssetDatabase.SaveAssets();
    }
}