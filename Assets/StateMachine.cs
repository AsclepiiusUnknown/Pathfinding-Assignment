using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StateMachine : MonoBehaviour
{
    #region Variables
    public enum State
    {
        Patrol,
        Seek,
        Attack,
        Flee,
    }

    [Header("General")]
    public State state;
    public GameObject AI;
    public GameObject Player;
    public Material playerMat;
    private Enemy enemyScript;
    public Player playerScript;
    private float checkTimer;
    public int checkTimerValue;
    public float normalSpeed, runningSpeed;

    [Header("Patrol State")]
    private PatrolAI patrolAI;

    [Header("Seek State")]
    private MoveAI moveAI;
    [Range(0, 25)]
    public float moveRadiusSize;
    public bool isSeeking = false;
    public float seekDistance;

    [Header("Attack State")]
    private AttackAI attackAI;
    public bool isAttacking;
    public float attackRadiusSize;

    [Header("FleeState")]
    private FleeAI fleeAI;
    #endregion

    private void Start()
    {
        moveAI = GetComponent<MoveAI>();
        patrolAI = GetComponent<PatrolAI>();
        attackAI = GetComponent<AttackAI>();
        fleeAI = GetComponent<FleeAI>();
        enemyScript = GetComponent<Enemy>();

        isSeeking = GetComponent<MoveAI>().isSeeking;

        checkTimer = checkTimerValue;

        if (moveAI == null)
        {
            Debug.LogError("moveAI not attached to StateMachine");
        }
        if (patrolAI == null)
        {
            Debug.LogError("patrolAI not attached to StateMachine");
        }

        ChangeStateTo("Patrol");
    }

    private void Update()
    {
        if (checkTimer <= 0)
        {
            checkTimer = checkTimerValue;
        }
        checkTimer -= Time.deltaTime;

        seekDistance = Vector3.Distance(AI.transform.position, Player.transform.position);
    }

    #region Patrol
    IEnumerator PatrolState()
    {
        Debug.Log("Patrol: Enter");
        state = State.Patrol;
        while (state == State.Patrol)
        {
            patrolAI.isPatrolling = true;
            playerMat.SetColor("_Patrol", Color.red);
            patrolAI.Patrol();

            CheckForChange();

            yield return 0; //Wait one frame
        }
        ChangeStateTo(state.ToString());
    }
    #endregion

    #region Seek
    public IEnumerator SeekState()
    {
        Debug.Log("Seek: Enter");
        state = State.Seek;
        while (state == State.Seek)//looping
        {
            isSeeking = true;
            playerMat.SetColor("_Seek", Color.red);
            moveAI.Move();

            CheckForChange();

            yield return 0;
        }
        ChangeStateTo(state.ToString());
    }
    #endregion

    #region Attack
    IEnumerator AttackState()
    {
        Debug.Log("Attack: Enter");
        state = State.Attack;
        while (state == State.Attack)//looping
        {
            isAttacking = true;
            playerMat.SetColor("_Attack", Color.red);
            attackAI.Attack();

            CheckForChange();

            yield return 0;
        }
        ChangeStateTo(state.ToString());
    }
    #endregion

    #region Flee
    IEnumerator FleeState()
    {
        state = State.Flee;
        Debug.Log("Flee: Enter");
        while (state == State.Flee)
        {
            fleeAI.isFleeing = true;

            CheckForChange();

            yield return 0;
        }
        ChangeStateTo(state.ToString());
    }
    #endregion

    void ChangeStateTo(string methodName)
    {
        StartCoroutine(methodName + "State");
    }

    void CheckForChange()
    {
        if ((enemyScript.health < (enemyScript.maxHealth / 4)) || enemyScript.health < playerScript.health)
        {
            Debug.Log("Exiting State");
            Debug.Log("Changing to Flee");

            state = State.Flee;

            patrolAI.isPatrolling = false;
            isSeeking = false;
            isAttacking = false;

            fleeAI.isFleeing = true;
        }
        else if (Vector3.Distance(AI.transform.position, Player.transform.position) <= attackRadiusSize)
        {
            Debug.Log("Exiting State");
            Debug.Log("Changing to Attack");

            state = State.Attack;

            patrolAI.isPatrolling = false;
            isSeeking = false;
            fleeAI.isFleeing = false;

            isAttacking = true;
        }
        else if (Vector3.Distance(AI.transform.position, Player.transform.position) <= moveRadiusSize)
        {
            Debug.Log("Exiting State");
            Debug.Log("Changing to Seek");

            state = State.Seek;

            patrolAI.isPatrolling = false;
            isAttacking = false;
            fleeAI.isFleeing = false;

            isSeeking = true;
        }
        else
        {
            Debug.Log("Exiting State");
            Debug.Log("Changing to Patrol");

            state = State.Patrol;

            isSeeking = false;
            fleeAI.isFleeing = false;
            isAttacking = false;

            patrolAI.isPatrolling = true;
        }
    }
}