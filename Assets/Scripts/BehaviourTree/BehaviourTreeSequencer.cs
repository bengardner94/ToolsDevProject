using UnityEngine;

public class BTSequencer : BTComposite
{
    [HideInInspector] public bool firstRun = true;
    public override BTState Process()
    {
        if (firstRun == true)
        {
            m_ActiveChild = 0;
            firstRun = false;
        }
        BTState ret = m_Children[m_ActiveChild].Process();

        if (ret == BTState.SUCCESS)
        {
            m_ActiveChild++;
            if (m_ActiveChild == m_Children.Count)
            {
                firstRun = true;
                return ret;
            }
            return BTState.PROCESSING;
        }
        else if (ret == BTState.FAILURE)
        {
            firstRun = true;
            return ret;
        }
        return ret;
    }

    public override string GetDescription()
    {
        return ("Processes all children until all return success or one returns failure");
    }
}