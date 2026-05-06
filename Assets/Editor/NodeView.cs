using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class NodeView : VisualElement
{
    
    public int m_ID;
    public string m_Name;
    public BTNode m_Node;
    public BehaviourTreeView m_BTreeView;
    

    public NodeView(int ID, string name, BTNode node, BehaviourTreeView bTreeView)
    {
        m_ID = ID;
        m_Name = name;
        m_Node = node;
        m_BTreeView = bTreeView;
    }

    void CreateManipulator()
    {
        this.AddManipulator(new ContextualMenuManipulator((evt) =>
            
        }
}