using UnityEngine;

public abstract class SteeringBehaviour : MonoBehaviour
{

    [Header("Settings")]
    public bool m_Active = true;
    public float m_Weight = 5;

    protected Vector2 m_DesiredVelocity;
    protected Vector2 m_Steering;
    protected Vector2 m_Velocity;
    protected float m_MaxSpeed;

    public abstract Vector2 CalculateForce();
}
