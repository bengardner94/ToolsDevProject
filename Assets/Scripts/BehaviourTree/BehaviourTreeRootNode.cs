using UnityEngine;

public class BTRootNode : BTNode
{
    public override BTState Process()
    {
        while (true)
        {
            BTState state = m_Children[0].Process();

            if (state == BTState.SUCCESS)
            {
                Debug.Log("completed");
                return state;
            }

            if (state == BTState.FAILURE)
            {
                Debug.Log("failed");
                return state;
            }
        }
    }

    public override string GetDescription()
    {
        return ("Root of the tree, acts same as infinite repeater");
    }
}