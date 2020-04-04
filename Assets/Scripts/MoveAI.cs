using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveAI : MonoBehaviour
{
    public Transform goal; //the goal the AI will travel to
    private NavMeshAgent agent; //the AI nav mech agent
    private StateMachine stateMachine; //a refernce to the state machine script
    public GameObject AI, Player; //slots for the AI and player gameobjects
    private float moveRadiusSize; //the size of the move radius
    public bool isSeeking; //whether or not the AI is in seek state

    void Start()
    {
        //set the variables to thier intended components
        stateMachine = GetComponent<StateMachine>();
        agent = GetComponent<NavMeshAgent>();
        AI = stateMachine.AI;
        Player = stateMachine.Player;
        moveRadiusSize = stateMachine.moveRadiusSize;

        if (goal == null) //debug the goal
        {
            Debug.LogError("AImove could not find the NavMeshAgent or GameObject");
        }
    }

    public void Move()
    {
        if (Vector3.Distance(AI.transform.position, Player.transform.position) <= moveRadiusSize) //double check that the requirements are met for seeking
        {
            if (agent.destination != goal.position) //check we arent already at the goal
            {
                isSeeking = true; //we are seeking
                agent.SetDestination(goal.position); //move the gameobject to the goal
                agent.speed = stateMachine.runningSpeed; //set the movement speed to the running speed held in statemachine
            }
        }
        else//otherwise
        {
            Stop();//call Stop()
        }
    }

    public void Stop()
    {
        if (goal != null) //as long as the goal is set
        {
            if (agent.destination != goal.position)
            {
                agent.SetDestination(agent.transform.position); //stop moving
                agent.speed = stateMachine.normalSpeed; //set the speed back to the normal speed held in stae machine
                isSeeking = false; //we are no longer seeking
            }
        }
    }
}
