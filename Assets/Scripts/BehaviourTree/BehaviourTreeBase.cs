using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public enum BTState
{
    SUCCESS,
    FAILURE,
    PROCESSING
}

public abstract class BTNode
{
    public BTState m_State;
    public List<BTNode> m_Children;
    public int m_ActiveChild;
    public Blackboard m_Blackboard;

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

public class BTree
{
    BTNode m_Root;
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
}