using UnityEngine;

public class BehaviourTreeMoveTo : BTNode
{
    string targetPosition = "TargetPos";
    string seekBehaviour = "SeekBehaviour";
    string playerPosition = "PlayerPosition";

    Vector2 previousPosition = Vector2.negativeInfinity;

    public override BTState Process()
    {
        Vector2 targetPos = (Vector2)m_Blackboard.GetFromDictionary(targetPosition);
        SteeringBehaviour_Seek seek = (SteeringBehaviour_Seek)m_Blackboard.GetFromDictionary(seekBehaviour);
        Vector2 playerPos = (Vector2)m_Blackboard.GetFromDictionary(playerPosition);

        if (previousPosition == playerPos)
        {
            return BTState.FAILURE;
        }

        seek.m_TargetPosition = targetPos;
        previousPosition = playerPos;

        if ((targetPos - playerPos).magnitude > 0.1f)
        {
            return BTState.SUCCESS;
        }

        return BTState.PROCESSING;
    }
}