using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class BehaviourTreeItem : VisualElement
{
    public int m_ID;
    public string m_Name;
    public BTNode m_Node;
    public int m_ChildList;
    public BehaviourTreeItem m_Parent;
    

    public BehaviourTreeItem(int ID, string name, BTNode node, BehaviourTreeItem parent)
    {
        m_ID = ID;
        m_Name = name;
        m_Node = node;
        m_Parent = parent;
    }

}