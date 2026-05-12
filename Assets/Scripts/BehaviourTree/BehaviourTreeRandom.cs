using UnityEngine;

public class BTRandom : BTComposite
{
    [HideInInspector] public bool firstRun = true;
    public override BTState Process()
    {
        m_ActiveChild = Random.Range(0, m_Children.Count);
        BTState ret = m_Children[m_ActiveChild].Process();
        return ret;
    }

    public override string GetDescription()
    {
        return ("Processes a child at random");
    }
}