using UnityEngine;
using System.Collections;
using System.Linq;

public class ConveyorMovement : MonoBehaviour {

	public GameObject[] waypoint;
	public float speed = 4.0f;
	public bool loop = true;
	public float dampingLook = 0.0f;
	public float pauseDuration = 0.0f;
	public bool isMoving = true;
	public string waypointSet;
	private float curTime;
	public int currentWaypoint = 0;
	//private CharacterController beltBlock;

	// Use this for initialization
	void Start () {
	
		//beltBlock = GetComponent<CharacterController>();

		//load waypoint array based on waypointSet
		if (waypointSet == "A") {
			waypoint = GameObject.FindGameObjectsWithTag ("WaypointSetA").OrderBy (go => go.name).ToArray ();
		} else if (waypointSet == "B") {
			waypoint = GameObject.FindGameObjectsWithTag ("WaypointSetB").OrderBy (go => go.name).ToArray ();
		}else if (waypointSet == "C") {
			waypoint = GameObject.FindGameObjectsWithTag ("WaypointSetC").OrderBy (go => go.name).ToArray ();
		}else if (waypointSet == "D") {
			waypoint = GameObject.FindGameObjectsWithTag ("WaypointSetD").OrderBy (go => go.name).ToArray ();
		}
	}
	
	// Update is called once per frame
	void Update () {
	
		//moving
		if (isMoving) {
			if (currentWaypoint < waypoint.Length) {
				moveBelt ();
			} else {    
				if (loop) {
					currentWaypoint = 0;
				} 
			}
		}
	}

	void moveBelt(){

		Vector3 target = waypoint [currentWaypoint].transform.position;
		Vector3 moveDirection = target - transform.position;
		
		if(moveDirection.magnitude < 0.5f){
			if (curTime == 0)
				curTime = Time.time; // Pause over the Waypoint
			if ((Time.time - curTime) >= pauseDuration){
				currentWaypoint++;
				curTime = 0;
			}
		}else{        
			var rotation = Quaternion.LookRotation(target - transform.position);
			transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * dampingLook);
			transform.Translate (moveDirection.normalized * speed * Time.deltaTime, Space.World); //Test translate

			//beltBlock.Move(moveDirection.normalized * speed * Time.deltaTime);
		}  
	}

	void OnTriggerEnter(Collider other){

		if (other.tag == "Player" || other.tag == "Pushable" || other.tag == "Magnetic") {
			other.transform.parent = gameObject.transform;
		}
		if (other.tag == "Cleaner") {
			Destroy (gameObject);
		}
	}

	void OnTriggerExit(Collider other){
		other.transform.parent = null;
	}
}
