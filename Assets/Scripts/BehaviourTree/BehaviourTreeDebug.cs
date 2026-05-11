using UnityEngine;

public class BTDebug : BTAction
{
    public string m_Debug;

    public override BTState Process()
    {
        Debug.Log(m_Debug);
        return BTState.SUCCESS;
    }

    public override string GetDescription()
    {
        return ("Outputs a string to the console");
    }
}