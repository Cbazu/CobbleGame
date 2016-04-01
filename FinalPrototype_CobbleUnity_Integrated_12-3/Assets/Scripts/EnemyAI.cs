using UnityEngine;
using System.Collections;


/******************************************************************************
*   Script modified from: http://docs.unity3d.com/Manual/nav-AgentPatrol.html *
******************************************************************************/

public class EnemyAI : MonoBehaviour {

    //public variables
    public float patrolSpeed = 2f;
    public float chaseSpeed = 5f;
    public bool patrolIsLoop = false;
    public GameObject[] patrolWayPoints;

    //private variables
    private NavMeshAgent agent;
    private int currentPoint;
    private int pointMultiplier=1; //dictates direction of travel when not traveling in a loop
    private float timeToWait = 0.0f;
    private float timeWaited = 0.0f;
    private bool isWaiting = false;
    private bool isTurning = false;
    private float strength;
    private Quaternion heading;

	// Use this for initialization
	void Start () {

        agent = GetComponent<NavMeshAgent>();
        agent.speed = patrolSpeed;
        currentPoint = 0;
        if (patrolWayPoints.Length != 0) { transform.position = patrolWayPoints[currentPoint].transform.position; }
        AssignNextPoint();
	}
	
	// Update is called once per frame
	void Update () {
	
        //waits before moving on to the next point
        if (isWaiting)
        {
            if (timeWaited >= timeToWait)
            {
                isWaiting = false;
                isTurning = true;
                //heading = Quaternion.LookRotation(patrolWayPoints[currentPoint].transform.position - transform.position);
            }
            else
            {
                timeWaited += Time.deltaTime;
            }
        }
        //Choose next destination point when target gets close to current one
        else if (agent.remainingDistance < 0.05f && !isTurning)
        {
            AssignNextPoint();
        }

        //turns player before proceeding on to next waypoint
        if (isTurning)
        {
            heading = Quaternion.LookRotation(patrolWayPoints[currentPoint].transform.position - transform.position);
            strength = Mathf.Min(agent.angularSpeed * Time.deltaTime, 1);
            transform.rotation = Quaternion.Lerp(transform.rotation, heading, strength);
            if (timeWaited > (timeToWait + 1.5f))
            {
                timeWaited = 0.0f;
                GotoNextPoint();
                isTurning = false;
            }
            timeWaited += Time.deltaTime;
        }
    }


    //Assigns wait time for the next way point
    void AssignNextPoint()
    {
        //returns if no points have been setup
        if (patrolWayPoints.Length == 0) { return; }

        //Wait until it's time to move to the next point
        //agent.destination = patrolWayPoints[currentPoint].transform.position;
        timeToWait = patrolWayPoints[currentPoint].GetComponent<WaypointInfo>().GetWaitTime();
        isWaiting = timeToWait != 0;
        Debug.Log("wait time is: " + timeToWait);

        //Choose the next point when agent gets close to current one
        if (patrolIsLoop)
        {
            currentPoint = (currentPoint + 1) % patrolWayPoints.Length;
        }
        else
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

    //Moves agent to the next point
    void GotoNextPoint()
    {

        //Move to next point
        agent.destination = patrolWayPoints[currentPoint].transform.position;
    }

}
