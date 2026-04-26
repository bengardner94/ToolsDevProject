using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringBehaviour_Manager : MonoBehaviour
{
    public MovingEntity m_Entity { get; private set; }
    public float m_MaxForce = 500;
    public float m_RemainingForce;
    public float m_Activation = 1f;
    public List<SteeringBehaviour> m_SteeringBehaviours;

	private void Awake()
	{
        m_Entity = GetComponent<MovingEntity>();

        if(!m_Entity)
            Debug.LogError("Steering Behaviours only working on type moving entity", this);
    }

	public Vector2 GenerateSteeringForce()
    {
        m_RemainingForce = m_MaxForce;
        Vector2 force = new Vector2(0, 0);
        foreach (SteeringBehaviour steeringBehaviour in m_SteeringBehaviours)
        {
            if (steeringBehaviour.m_Active)
            {
                Vector2 newForce = steeringBehaviour.CalculateForce() * m_Activation;
                m_RemainingForce -= Maths.Magnitude(newForce);
                if (m_RemainingForce < 0)
                {
                    m_RemainingForce += Maths.Magnitude(newForce);
                    force += (Maths.Normalise(newForce) * m_RemainingForce);
                    break;
                }
                else
                {
                    force += newForce;
                }
            }
        }
        return force;
    }

    public void EnableExclusive(SteeringBehaviour behaviour)
	{
        if(m_SteeringBehaviours.Contains(behaviour))
		{
            foreach(SteeringBehaviour sb in m_SteeringBehaviours)
			{
                sb.m_Active = false;
			}

            behaviour.m_Active = true;
		}
        else
		{
            Debug.Log(behaviour + " doesn't not exist on object", this);
		}
	}
    public void DisableAllSteeringBehaviours()
    {
        foreach (SteeringBehaviour sb in m_SteeringBehaviours)
        {
            sb.m_Active = false;
        }
    }

    public void AddSteeringBehaviour(SteeringBehaviour behaviour) 
    {
        m_SteeringBehaviours.Add(behaviour);
    }

    public void RemoveSteeringBehaviour(SteeringBehaviour behaviour)
    {
        m_SteeringBehaviours.Remove(behaviour);
    }
}
