using System.Collections.Generic;
using UnityEngine;

public abstract class BTComposite : BTNode
{
    [HideInInspector] public int m_ActiveChild;
}