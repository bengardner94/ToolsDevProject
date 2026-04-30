using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

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

    public abstract BTState Process();

    public abstract void AddBBRecursive(Blackboard bb);
}