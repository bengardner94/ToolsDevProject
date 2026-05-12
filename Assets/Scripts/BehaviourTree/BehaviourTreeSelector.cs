using UnityEngine;

public class BTSelector : BTComposite
{
    [HideInInspector] public bool firstRun = true;
    public override BTState Process()
    {
        if (firstRun)
        {
            m_ActiveChild = 0;
            firstRun = false;
        }
        BTState ret = m_Children[m_ActiveChild].Process();

        if (ret == BTState.SUCCESS)
        {
            firstRun = true;
            return ret;
        }
        else if (ret == BTState.FAILURE)
        {
            m_ActiveChild++;
            if (m_ActiveChild == m_Children.Count)
            {

                firstRun = true;
                return ret;
            }
            return BTState.PROCESSING;
        }

        return ret;
    }

    public override string GetDescription()
    {
        return ("Processes all children until one returns success or all return failure");
    }
}
