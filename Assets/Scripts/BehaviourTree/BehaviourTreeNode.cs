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


    public BTState m_State;
    public Blackboard m_Blackboard;

    int m_ID;
    public string m_Name;
    int m_Depth;
    public BTNode m_Node;
    public BTNode m_Parent;
    public List<BTNode> m_Children;




    public abstract BTState Process();

    public void AddBBRecursive(Blackboard bb)
    {
        m_Blackboard = bb;

        foreach (BTNode child in m_Children)
        {
            child.AddBBRecursive(bb);
        }
    }
}