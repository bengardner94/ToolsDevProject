using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class BTNode : ScriptableObject
{
    public enum BTState
    {
        SUCCESS,
        FAILURE,
        PROCESSING
    }


    BTState m_State;
    [HideInInspector] public Blackboard m_Blackboard;

    [HideInInspector] public string m_Description;
    [HideInInspector] public BTNode m_Node;
    [HideInInspector] public BTNode m_Parent;
    [HideInInspector] public List<BTNode> m_Children = new List<BTNode>();




    public abstract BTState Process();

    public abstract string GetDescription();

    public void AddBBRecursive(Blackboard bb)
    {
        m_Blackboard = bb;

        foreach (BTNode child in m_Children)
        {
            child.AddBBRecursive(bb);
        }
    }
}