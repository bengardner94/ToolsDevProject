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
    public BehaviourTreeView m_BTreeView;
    

    public BehaviourTreeItem(int ID, string name, BTNode node)
    {
        m_ID = ID;
        m_Name = name;
        m_Node = node;
    }

}