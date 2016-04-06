using UnityEngine;
using System.Collections;

public class ElectroMagnet : MonoBehaviour {

	GameObject magObject;
	GameObject magnet;
	private float startTime;
	private bool pickUp = false;
	private bool letGo = false;
	GameObject player;
	public Transform target;

	// Use this for initialization
	void Start () {
		magnet = this.gameObject;
		player = GameObject.FindGameObjectWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update () {
	
		if (pickUp == true) {
			magObject.transform.position = Vector3.Lerp (magObject.transform.position, magnet.transform.position, (Time.time - startTime) / 2f);
			StartCoroutine (Wait ());
		} else if (letGo == true) {
			ThrowPlayer ();
		}
	}

	void OnTriggerEnter(Collider other){
		if (other.tag == "Player") {
			pickUp = true;
			magObject = other.gameObject;
			startTime = Time.time;
		}
	}

	IEnumerator Wait(){
		Debug.Log ("Waiting");
		yield return new WaitForSeconds (2.5f);
		letGo = true;
		pickUp = false;
	}

	void ThrowPlayer(){
		player.GetComponent<Rigidbody> ().AddForce ((target.position - transform.position) * 2000f);
		letGo = false;
	}

}
