using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringBehaviour_Flee : SteeringBehaviour
{
    public Transform m_FleeTarget;
    public float m_FleeRadius;

    public float m_distance;

    public override Vector2 CalculateForce()
    {
        if (m_FleeTarget)
        {
            m_distance = Maths.Magnitude(new Vector2(m_FleeTarget.position.x - transform.position.x, m_FleeTarget.position.y - transform.position.y));
            m_DesiredVelocity = new Vector2(m_FleeTarget.position.x - transform.position.x, m_FleeTarget.position.y - transform.position.y);
            m_DesiredVelocity = Maths.Normalise(m_DesiredVelocity) * m_MaxSpeed;
            m_Steering = (-(m_DesiredVelocity - m_Manager.m_Entity.m_Velocity));
            return (m_Steering * Mathf.Lerp(m_Weight, 0, Mathf.Min(m_distance, m_FleeRadius) / m_FleeRadius));
        }
        
        return Vector2.zero;
    }
}
