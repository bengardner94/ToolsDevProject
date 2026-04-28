using UnityEngine;

public class BehaviourTreeSequencer : BTNode
{
    public override BTState Process()
    {
        BTState ret = m_Children[m_ActiveChild].Process();

        if (ret == BTState.SUCCESS)
        {
            m_ActiveChild++;
            if (m_ActiveChild == m_Children.Count)
            {
                return ret;
            }
            return BTState.PROCESSING;
        }
        else if (ret == BTState.FAILURE)
        {
            return ret;
        }

        return ret;
    }
}