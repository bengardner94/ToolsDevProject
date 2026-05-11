public class BTInfRepeater : BTDecorator
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

    public override string GetDescription()
    {
        return ("Loops infinitely until failure");
    }
}