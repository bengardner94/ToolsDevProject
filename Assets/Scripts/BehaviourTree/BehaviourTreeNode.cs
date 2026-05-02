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

    [SerializeField] int m_ID;
    [SerializeField] string m_Name;
    [SerializeField] int m_Depth;
    [SerializeField] public BTNode m_Node;
    [NonSerialized] public BTNode m_Parent;
    [NonSerialized] public List<BTNode> m_Children;




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