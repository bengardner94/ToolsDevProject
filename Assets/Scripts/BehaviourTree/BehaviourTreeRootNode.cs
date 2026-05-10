public class BTRootNode : BTNode
{
    public override BTState Process()
    {
        while (true)
        {
            BTState state = m_Children[0].Process();

            if (state == BTState.FAILURE)
            {
                return state;
            }
        }
    }
}