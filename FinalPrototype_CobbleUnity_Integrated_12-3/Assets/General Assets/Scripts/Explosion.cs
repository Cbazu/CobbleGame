using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {

	public float timeLeft = 10.0f;
	public float expTime = 5.0f;
	public bool isExploding = false;
	public bool playerInRange = false;
	GameObject expObj;

	// Use this for initialization
	void Start () {
		expObj = gameObject;
		expObj.GetComponent<ParticleSystem> ().enableEmission = false;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		timeLeft -= Time.deltaTime;

		if (expTime > 0f) {
			expTime -= Time.deltaTime;
			isExploding = true;
			//Kill player if in range
			if(playerInRange == true){
				//gameObject.SendMessage("Die",SendMessageOptions.DontRequireReceiver);
				GameManager.Notifications.PostNotification(this, "Die");
				Debug.Log ("Die");
			}

		} else if (expTime <= 0f) {
			expObj.GetComponent<ParticleSystem> ().enableEmission = false;
			isExploding = false;
		}
		//Debug.Log (timeLeft);
	
		if(timeLeft < 0.0f){
			Explode ();
			//isExploding = true;
		}
	}

	void Explode(){
		//expObj.gameObject.GetComponent<MeshRenderer> ().enabled = true;
		expObj.GetComponent<ParticleSystem> ().enableEmission = true;
		timeLeft = 10.0f;
		expTime = 5.0f;

		//isExploding = false;

		//  Kill player if in explosion radius
		if(playerInRange == true){
		//	gameObject.SendMessage("Die",SendMessageOptions.DontRequireReceiver);
		}
		 
	}

	void OnTriggerEnter(Collider other){
		if (other.tag == "Player")
			playerInRange = true;
	}

	void OnTriggerExit(Collider other){
		if (other.tag == "Player")
			playerInRange = false;
	}

}
