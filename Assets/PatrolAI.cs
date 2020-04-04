using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolAI : MonoBehaviour
{
    private StateMachine stateMachine;
    private GameObject AI;
    private float radiusSize;
    public bool isPatrolling = true;
    public GameObject[] Waypoint;
    private int currentWaypoint = 0;
    public float speed = 5.0f;
    public float MinDistanceToWaypoint = .5f;
    private NavMeshAgent agent;


    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        stateMachine = GetComponent<StateMachine>();
        AI = stateMachine.AI;
        agent.speed = speed;
    }

    public void Update()
    {
        if (AI == null)
        {
            Debug.LogError("AI");
        }
        if (Waypoint == null)
        {
            Debug.LogError("Waypoint");
        }
    }

    public void Patrol()
    {
        if (AI != null)
        {
            if (isPatrolling)
            {
                if (Vector3.Distance(AI.transform.position, Waypoint[currentWaypoint].transform.position) < MinDistanceToWaypoint)
                {
                    currentWaypoint++;
                }

                if (currentWaypoint >= Waypoint.Length)
                {
                    currentWaypoint = 0;
                }

                MoveAI(Waypoint[currentWaypoint].transform.position);
            }
            else
            {
                agent.speed = stateMachine.normalSpeed;
            }
        }
    }

    public void MoveAI(Vector3 targetPosition)
    {
        agent.SetDestination(targetPosition);
        agent.speed = stateMachine.normalSpeed;
    }
}
