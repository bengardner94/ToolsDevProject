using UnityEngine;

public class BehaviourTreeSelector : BTNode
{
    public override BTState Process()
    {
        BTState ret = m_Children[m_ActiveChild].Process();

        if (ret == BTState.SUCCESS)
        {
            return ret;
        }
        else if (ret == BTState.FAILURE)
        {
            m_ActiveChild++;
            if (m_ActiveChild == m_Children.Count)
            {
                return ret;
            }
            return BTState.PROCESSING;
        }

        return ret;
    }
}
