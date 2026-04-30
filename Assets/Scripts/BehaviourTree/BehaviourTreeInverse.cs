public class BTInverse : BTDecorator
{
    public override BTState Process()
    {
        BTState ret = m_Child.Process();

        if (ret == BTState.SUCCESS)
        {
            return BTState.FAILURE;
        }
        if (ret == BTState.FAILURE)
        {
            return BTState.SUCCESS;
        }

        return ret;
    }
}