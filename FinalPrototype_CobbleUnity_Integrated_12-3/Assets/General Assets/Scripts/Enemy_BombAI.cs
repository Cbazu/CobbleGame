using UnityEngine;
using System.Collections;

public class Enemy_BombAI : MonoBehaviour {

	public Transform[] waypoint;
	public float patrolSpeed = 3.0f;
	public float attackSpeed = 10.0f;
	public bool loop = true;
	public float dampingLook = 6.0f;
	public float pauseDuration = 0.0f;
	public bool warning = false;
	public AudioClip warningSound;
	public AudioClip boomSound;
	public bool isPatrolling = true;
	public bool isAttacking = false;
	public GameObject player;
	private float curTime;
	public int currentWaypoint = 0;
	private CharacterController character;
	private AudioSource source;

	void  Start (){
		
		character = GetComponent<CharacterController>();
		source = GetComponent<AudioSource> ();
	}
	
	void  Update (){

		//patrol
		if (isPatrolling) {
			if (currentWaypoint < waypoint.Length) {
				patrol ();
			} else {    
				if (loop) {
					currentWaypoint = 0;
				} 
			}
		} else if (isAttacking) {
			Attack ();
		}

		//attack




		//warning
		if (warning == true) {
			WarningOn ();
		} else if (warning == false) {
			WarningOff();
		}
	}
	
	void  patrol (){
		
		Vector3 target = waypoint[currentWaypoint].position;
		target.y = transform.position.y; // Keep waypoint at character's height
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
			character.Move(moveDirection.normalized * patrolSpeed * Time.deltaTime);
		}  
	}

	void Attack(){
		Vector3 target = player.transform.position;
		Vector3 moveDirection = target - transform.position;

		character.Move(moveDirection.normalized * attackSpeed * Time.deltaTime);

	}

	void WarningOn(){
		//source.PlayOneShot (warningSound, .5f);
			source.enabled = true;
	}

	void WarningOff(){
		source.enabled = false;
	}

	void OnCollisionEnter(Collision col){
		if (col.gameObject.tag == "Player") {
			source.PlayOneShot (boomSound, 1f);
			GameManager.Notifications.PostNotification (this, "Die");
		}
		if (col.gameObject.tag == "IntObject") {
			currentWaypoint = currentWaypoint + 1;
			Debug.Log ("Previous waypoint");
		}

	}
}
