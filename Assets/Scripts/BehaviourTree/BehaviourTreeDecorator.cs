public abstract class BTDecorator : BTNode
{
    public BTNode m_Child;

    public override void AddBBRecursive(Blackboard bb)
    {
        m_Blackboard = bb;

        m_Child.AddBBRecursive(bb);
    }
}