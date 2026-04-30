

public class BTInfRepeater : BTDecorator
{
    public override BTState Process()
    {
        while (true)
        {
            BTState state = m_Child.Process();

            if (state == BTState.FAILURE)
            {
                return state;
            }
        }
    }
}