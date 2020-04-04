using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveAI : MonoBehaviour
{
    public Transform goal;
    private NavMeshAgent agent;
    private StateMachine stateMachine;
    private GameObject AI, Player;
    private float moveRadiusSize;
    public bool isSeeking;

    // Start is called before the first frame update
    void Start()
    {
        stateMachine = GetComponent<StateMachine>();
        agent = GetComponent<NavMeshAgent>();
        AI = stateMachine.AI;
        Player = stateMachine.Player;
        moveRadiusSize = stateMachine.moveRadiusSize;

        if (goal == null)
        {
            Debug.LogError("AImove could not find the NavMeshAgent or GameObject");
        }
    }

    private void Update()
    {
        //Move();
    }

    public void Move()
    {
        if (Vector3.Distance(AI.transform.position, Player.transform.position) <= moveRadiusSize)
        {
            if (agent.destination != goal.position)
            {
                isSeeking = true;
                agent.SetDestination(goal.position);
                agent.speed = stateMachine.runningSpeed;
            }
        }
        else
        {
            Stop();
            agent.speed = stateMachine.normalSpeed;
        }
    }

    public void Stop()
    {
        if (goal != null)
        {
            if (agent.destination != goal.position)
            {
                agent.SetDestination(agent.transform.position);
                isSeeking = false;
            }
        }
    }
}
