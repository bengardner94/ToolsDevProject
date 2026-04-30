using System.Collections.Generic;

public abstract class BTComposite : BTNode
{
    public List<BTNode> m_Children;
    public int m_ActiveChild;

    public override void AddBBRecursive(Blackboard bb)
    {
        m_Blackboard = bb;

        foreach (BTNode child in m_Children)
        {
            child.AddBBRecursive(bb);
        }
    }
}