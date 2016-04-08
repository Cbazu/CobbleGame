using UnityEngine;
using System.Collections;


/**************************************************************************************
*   Script heavily modified from: http://docs.unity3d.com/Manual/nav-AgentPatrol.html *
**************************************************************************************/

[RequireComponent (typeof(NavMeshAgent))]

public class EnemyAI : MonoBehaviour {

    //public variables
    public Transform enemyVision;
    public float patrolSpeed = 2f;
    public float chaseSpeed = 5f;
    public bool patrolIsLoop = false;
    public GameObject[] patrolWayPoints;
    public GameObject player;
    public float visibleDistance = 10.0f;

    //private variables
    private NavMeshAgent agent;
    private int currentPoint;
    private int pointMultiplier=1; //dictates direction of travel when not traveling in a loop
    private float timeToWait = 0.0f;
    private float timeWaited = 0.0f;
    private bool isWaiting = false;
    private bool isTurning = false;
    private float turnTime;
    private Transform heading;
    private GameObject chaseTarget;

    void Awake()
    {

        //variables
        Transform enemyVisionCheck;
        GameObject newObject;

        //Check for enemyVision prefab and if there isn't one create it
        enemyVisionCheck = transform.Find(enemyVision.name);
        if (enemyVisionCheck != null)
        {
            Debug.Log("Found EnemyVision!");
            Debug.Log("Name that was searched: " + enemyVisionCheck.name);
        }
        else
        {
            enemyVisionCheck = (Transform) Instantiate(enemyVision, transform.position, Quaternion.identity);
            enemyVisionCheck.transform.parent = transform;
        }
    }

    // Use this for initialization
    void Start () {

        agent = GetComponent<NavMeshAgent>();
        agent.speed = patrolSpeed;
        currentPoint = 0;
        if (patrolWayPoints.Length != 0) { GotoNextPoint(); }
	}
	
	// Update is called once per frame
	void Update () {

        if(chaseTarget != null)
        {
            GotoNextPoint(chaseTarget.transform);
        }
        //waits before moving on to the next point
        else if (isWaiting)
        {
            if (timeWaited >= timeToWait)
            {
                isWaiting = false;
                timeWaited = 0f;
                GotoNextPoint();
            }
            else
            {
                timeWaited += Time.deltaTime;
            }
        }

        //Choose next destination point when target gets close to current one once subject is finished turning
        else if (agent.remainingDistance < 0.05f /*&& !isTurning*/)
        {
            AssignNextPoint();
        }
    }


    //Assigns wait time for the next way point
    void AssignNextPoint()
    {
        //returns if no points have been setup
        if (patrolWayPoints.Length == 0) { return; }

        //Wait until it's time to move to the next point
        timeToWait = patrolWayPoints[currentPoint].GetComponent<WaypointInfo>().GetWaitTime();
        isWaiting = timeToWait != 0;
        Debug.Log("wait time is: " + timeToWait);
        Debug.Log("Current waypoint is " + currentPoint);
        //Choose the next point when agent gets close to current one
        if (patrolIsLoop)
        {
            currentPoint = (currentPoint + 1) % patrolWayPoints.Length; //loops through all the waypoints continuously
        }
        else //Performs a down and back route in order of the array
        {
            if (currentPoint == patrolWayPoints.Length - 1)
            {
                pointMultiplier = -1;
            }
            else if (currentPoint == 0)
            {
                pointMultiplier = 1;
            }
            currentPoint += pointMultiplier;
            Debug.Log("Moving to point " + currentPoint);
        }
    }

    void AssignNextPoint(Transform chasePlayer)
    {
        //Overloaded function used to chase the player or set a point manually.
        agent.destination = chasePlayer.position;
        agent.speed = chaseSpeed;
    }

    //Moves agent to the next point
    void GotoNextPoint()
    {
        //Move to next point
        agent.destination = patrolWayPoints[currentPoint].transform.position;
    }

    void GotoNextPoint(Transform chasePos)
    {
        agent.destination = chasePos.position;
    }

    public void ChaseTarget(GameObject target)
    {

        //set the chase target and speed
        chaseTarget = target;
        agent.speed = chaseSpeed;
    }

    public void ContinuePatrol()
    {
        agent.destination = patrolWayPoints[currentPoint].transform.position;
        agent.speed = patrolSpeed;
        chaseTarget = null; //removes target for effect loop selection
        //Debug.Log("Chase target is: "+chaseTarget.ToString());
    }
}
