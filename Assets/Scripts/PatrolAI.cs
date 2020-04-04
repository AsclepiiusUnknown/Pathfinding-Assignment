using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolAI : MonoBehaviour
{
    private StateMachine stateMachine; //refernce to the statemachine script
    private GameObject AI;//slots for the AI gameobject
    public bool isPatrolling = true;//whether or not the AI is in patrol state
    public GameObject[] Waypoint;//slots for any number of waypoint objects
    private int currentWaypoint = 0; //the waypoint index we are currently at
    public float speed = 5.0f; //the speed at which to move between the points
    public float MinDistanceToWaypoint = .5f; //the minimum distance to be from the waypoints position
    private NavMeshAgent agent; // the navMeshAgent


    private void Start()
    {
        //set the variables to thier intended components
        agent = GetComponent<NavMeshAgent>();
        stateMachine = GetComponent<StateMachine>();
        AI = stateMachine.AI;
        agent.speed = speed;
    }

    public void Update()
    {
        if (AI == null) //debug for the AI
        {
            Debug.LogError("AI");
        }
        if (Waypoint == null)//debugging for the waypoints
        {
            Debug.LogError("Waypoint");
        }
    }

    public void Patrol()
    {
        if (AI != null) //is AI is set
        {
            if (isPatrolling) //if we are meant to be patrolling
            {
                if (Vector3.Distance(AI.transform.position, Waypoint[currentWaypoint].transform.position) < MinDistanceToWaypoint) //if we are at the current waypoint
                {
                    currentWaypoint++; //set the current one to the next one
                }

                if (currentWaypoint >= Waypoint.Length)
                {
                    currentWaypoint = 0; // if we reach the end of our list go back to the start of our list
                }

                MoveAI(Waypoint[currentWaypoint].transform.position);//call upon MoveAI()
            }
            else //otherwise
            {
                agent.speed = stateMachine.normalSpeed;//set the speed to the normal speed
            }
        }
    }

    public void MoveAI(Vector3 targetPosition)
    {
        agent.SetDestination(targetPosition);//move to the target position
        agent.speed = stateMachine.normalSpeed; // at the normal speed
    }
}
