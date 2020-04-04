using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FleeAI : MonoBehaviour
{
    private NavMeshAgent agent;
    public GameObject player;
    public float fleeDistance;
    public bool isFleeing;
    public StateMachine stateMachine;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        stateMachine = GetComponent<StateMachine>();
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);
        //Debug.Log("Distance from player: " + distance);

        if (distance < fleeDistance && isFleeing)
        {
            Vector3 dirToPlayer = transform.position - player.transform.position;

            Vector3 newPos = transform.position + dirToPlayer;

            agent.SetDestination(newPos);
            agent.speed = stateMachine.runningSpeed;
        }
        else
        {
            agent.speed = stateMachine.normalSpeed;
        }
    }
}
