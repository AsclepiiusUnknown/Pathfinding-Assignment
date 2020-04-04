using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AttackAI : MonoBehaviour
{
    public Transform goal;
    private NavMeshAgent agent;
    private StateMachine stateMachine;
    public GameObject AI, Player;
    private float attackRadiusSize;
    public bool isAttacking;
    private Player playerScript;

    private void Awake()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        stateMachine = GetComponent<StateMachine>();
        AI = stateMachine.AI;
        agent = AI.GetComponent<NavMeshAgent>();
        Player = stateMachine.Player;
        attackRadiusSize = stateMachine.moveRadiusSize;
    }
    void Start()
    {
        if (goal == null)
        {
            Debug.LogError("AImove could not find the NavMeshAgent or GameObject");
        }
    }

    public void Attack()
    {
        if (Vector3.Distance(AI.transform.position, Player.transform.position) <= attackRadiusSize)
        {
            if (agent.destination != goal.position)
            {
                isAttacking = true;
                agent = AI.GetComponent<NavMeshAgent>();
                agent.SetDestination(goal.position);
                agent.speed = stateMachine.runningSpeed;
                playerScript.TakeDamage(69);
            }
        }
        else
        {
            Stop();
        }
    }

    public void Stop()
    {
        if (goal != null)
        {
            if (agent.destination != goal.position)
            {
                agent.SetDestination(agent.transform.position);
                agent.speed = stateMachine.normalSpeed;
                isAttacking = false;
            }
        }
    }
}
