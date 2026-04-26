using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SteeringBehaviour_Seek : SteeringBehaviour
{
    public Vector2 m_TargetPosition;

    public override Vector2 CalculateForce()
    {
        m_DesiredVelocity = new Vector2(m_TargetPosition.x - transform.position.x, m_TargetPosition.y - transform.position.y);
        m_DesiredVelocity = Maths.Normalise(m_DesiredVelocity) * m_Manager.m_Entity.m_MaxSpeed;
        m_Steering = (m_DesiredVelocity - m_Manager.m_Entity.m_Velocity) * m_Weight;
        return m_Steering;
    }
}
