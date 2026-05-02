using UnityEngine;

public class BTRepeater : BTDecorator
{
    public int m_Repeat;

    public override BTState Process()
    {
        for (int i = 0; i < m_Repeat; i++)
        {
            BTState ret = m_Children[0].Process();

            if (ret == BTState.FAILURE)
            {
                return ret;
            }
        }

        return BTState.SUCCESS;
    }
}