using UnityEngine;

public class BehaviourTreeMoveAway : BTNode
{
    string fleeTarget = "FleeTarget";
    string fleeBehaviour = "fleeBehaviour";
    string radius = "FleeRadius";
    string playerPosition = "PlayerPosition";

    Vector2 previousPosition = Vector2.negativeInfinity;

    public override BTState Process()
    {
        Transform target = (Transform)m_Blackboard.GetFromDictionary(fleeTarget);
        SteeringBehaviour_Flee flee = (SteeringBehaviour_Flee)m_Blackboard.GetFromDictionary(fleeBehaviour);
        float fleeRadius = (float)m_Blackboard.GetFromDictionary(radius);
        Vector2 playerPos = (Vector2)m_Blackboard.GetFromDictionary(playerPosition);

        if (previousPosition == playerPos)
        {
            return BTState.FAILURE;
        }

        flee.m_FleeTarget = target;
        flee.m_FleeRadius = fleeRadius;
        previousPosition = playerPos;

        if ((new Vector2(target.position.x, target.position.y) - playerPos).magnitude < fleeRadius)
        {
            return BTState.SUCCESS;
        }

        return BTState.PROCESSING;
    }
}