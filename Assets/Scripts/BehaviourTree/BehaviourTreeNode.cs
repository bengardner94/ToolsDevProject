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
    public Blackboard m_Blackboard;

    public string m_Description;
    public BTNode m_Node;
    public BTNode m_Parent;
    public List<BTNode> m_Children = new List<BTNode>();




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