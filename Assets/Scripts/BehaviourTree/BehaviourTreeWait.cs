using UnityEngine;
using System.Collections;
using System.Threading;

public class BTWait : BTAction
{
    public float waitDuration;
    float waitTime = 0;
    bool timerRun = true;

    public override BTState Process()
    {
        if (timerRun == true)
        {
            waitTime = Time.time;
            timerRun = false;
        }
        Debug.Log(Time.time - waitTime);
        if (Time.time - waitTime > waitDuration)
        {
            timerRun = true;
            return BTState.SUCCESS;
        }
        return BTState.PROCESSING;
    }

    public override string GetDescription()
    {
        return ("Causes the system to wait before processing");
    }
}