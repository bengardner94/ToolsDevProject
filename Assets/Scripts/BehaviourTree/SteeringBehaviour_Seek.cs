using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SteeringBehaviour_Seek : SteeringBehaviour
{
    [Header("Seek Properties")]
    [Header("Settings")]
    public Vector2 m_TargetPosition;

    [Space(10)]

    [Header("Debugs")]
    [SerializeField]
    protected Color m_Debug_TargetColour = Color.yellow;

    public override Vector2 CalculateForce()
    {
        m_DesiredVelocity = new Vector2(m_TargetPosition.x - transform.position.x, m_TargetPosition.y - transform.position.y);
        m_DesiredVelocity = Maths.Normalise(m_DesiredVelocity) * m_MaxSpeed;
        m_Steering = (m_DesiredVelocity - m_Velocity) * m_Weight;
        return m_Steering;
    }
}
