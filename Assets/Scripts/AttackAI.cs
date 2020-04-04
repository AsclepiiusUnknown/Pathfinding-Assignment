using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AttackAI : MonoBehaviour
{
    public Transform goal; //the goal the AI will travel to
    private NavMeshAgent agent; //the AI nav mech agent
    private StateMachine stateMachine; //a refernce to the state machine script
    public GameObject AI, Player; //slots for the AI and player gameobjects
    private float attackRadiusSize; //the size of the attack radius
    public bool isAttacking; //whether or not the AI is in attacking state
    private Player playerScript; //a refernce to the player script

    private void Awake()
    {
        //set the variables to thier intended components
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        stateMachine = GetComponent<StateMachine>();
        AI = stateMachine.AI;
        agent = AI.GetComponent<NavMeshAgent>();
        Player = stateMachine.Player;
        attackRadiusSize = stateMachine.moveRadiusSize;
    }
    void Start()
    { 
        //debug the goal
        if (goal == null)
        {
            Debug.LogError("AImove could not find the NavMeshAgent or GameObject");
        }
    }

    public void Attack()
    {
        if (Vector3.Distance(AI.transform.position, Player.transform.position) <= attackRadiusSize) //double check that the requirements are met for attacking
        {
            if (agent.destination != goal.position) //check we arent already at the goal
            {
                isAttacking = true; //we are attacking
                agent = AI.GetComponent<NavMeshAgent>(); //set the navmesh again just in case
                agent.SetDestination(goal.position); //move the gameobject to the goal
                agent.speed = stateMachine.runningSpeed; //set the movement speed to the running speed held in statemachine
                playerScript.TakeDamage(69); //tell the player to take damage as long as you are in this radius
            }
        }
        else //otherise
        {
            Stop(); //call on Stop()
        }
    }

    public void Stop()
    {
        if (goal != null) //as long as the goal is set
        {
            if (agent.destination != goal.position) //and the AI hasnt reached its goal
            {
                agent.SetDestination(agent.transform.position); //stop moving
                agent.speed = stateMachine.normalSpeed; //set the speed back to the normal speed held in stae machine
                isAttacking = false; //we are no longer attacking
            }
        }
    }
}
