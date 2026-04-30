using UnityEngine;

public abstract class MovingEntity : MonoBehaviour
{
    Rigidbody2D m_Rigidbody;
    public float m_MaxSpeed = 15.0f;
    public float m_MaxTurnDegrees = 90f;

    protected Animator m_Animator;
    protected SpriteRenderer m_Renderer;

    public Vector2 m_Velocity { get { return m_Rigidbody.linearVelocity; } }

	protected void Awake()
	{
        m_Animator = GetComponent<Animator>();

        m_Renderer = GetComponent<SpriteRenderer>();

        m_Rigidbody = GetComponent<Rigidbody2D>();
	}

	protected abstract Vector2 GenerateVelocity();

    protected virtual void FixedUpdate()
	{
        MoveAndRotate();
	}

	protected void MoveAndRotate()
	{
        Vector2 force = GenerateVelocity();

        if(force == Vector2.zero) { return; }

        Vector2 normalisedForce = force.normalized;

        Vector2 acceleration = force / m_Rigidbody.mass;

        m_Rigidbody.linearVelocity += acceleration * Time.fixedDeltaTime;
        m_Rigidbody.linearVelocity = Vector2.ClampMagnitude(m_Rigidbody.linearVelocity, m_MaxSpeed);

        transform.up = m_Rigidbody.linearVelocity.normalized;
    }
}
