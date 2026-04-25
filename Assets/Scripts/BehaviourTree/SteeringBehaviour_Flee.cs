using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringBehaviour_Flee : SteeringBehaviour
{
    [Header("Flee Properties")]
    [Header("Settings")]
    public Transform m_FleeTarget;
    public float m_FleeRadius;

    public float m_distance;

    [Space(10)]

    [Header("Debugs")]
    [SerializeField]
    protected Color m_Debug_RadiusColour = Color.yellow;

    public override Vector2 CalculateForce()
    {
        if (m_FleeTarget)
        {
            m_distance = Maths.Magnitude(new Vector2(m_FleeTarget.position.x - transform.position.x, m_FleeTarget.position.y - transform.position.y));
            if (m_FleeRadius > m_distance)
            {
                m_DesiredVelocity = new Vector2(m_FleeTarget.position.x - transform.position.x, m_FleeTarget.position.y - transform.position.y);
                m_DesiredVelocity = Maths.Normalise(m_DesiredVelocity) * m_MaxSpeed;
                m_Steering = (-(m_DesiredVelocity - m_Velocity));
                return (m_Steering * Mathf.Lerp(m_Weight, 0, Mathf.Min(m_distance, m_FleeRadius) / m_FleeRadius));
            }
        }
        
        return Vector2.zero;
    }
}
