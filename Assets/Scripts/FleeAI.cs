using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FleeAI : MonoBehaviour
{
    private NavMeshAgent agent; // the nav mesh agent
    public GameObject player;//the player object
    public float fleeDistance; //the distance at which to begin fleeing
    public bool isFleeing; //whether or not the AI is fleeing
    public StateMachine stateMachine; //refernce to the state machine script

    void Start()
    {
        //set the variables accorningly
        agent = GetComponent<NavMeshAgent>(); 
        stateMachine = GetComponent<StateMachine>();
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position); //regularly check the distance

        if (distance < fleeDistance && isFleeing) //if the AI is to flee and the distance if less than the flee distance variable
        {
            Vector3 dirToPlayer = transform.position - player.transform.position; //face away from the player

            Vector3 newPos = transform.position + dirToPlayer; //set newPos to the distance between the AI and player but in the opposite direction

            agent.SetDestination(newPos); //move to newPos
            agent.speed = stateMachine.runningSpeed; //move at the running speed
        }
        else//otherwise
        {
            agent.speed = stateMachine.normalSpeed;//move at normal speed
        }
    }
}
