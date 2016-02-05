using UnityEngine;
using System.Collections;

public class Droplet : MonoBehaviour {

	GameObject droplet;
	public float timer = 2f;

	// Use this for initialization
	void Start () {
		droplet = gameObject;
		droplet.GetComponent<Rigidbody> ().isKinematic = true;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (timer > 0f) {
			droplet.transform.localScale += new Vector3(0.0002f,0.0002f,0.0002f);
			timer -= Time.deltaTime;
		} else {
			Drop ();
		}
	}

	void Drop(){
		droplet.GetComponent<Rigidbody> ().isKinematic = false;
	}

	void OnTriggerEnter(Collider other){
		Destroy (droplet.gameObject);
	}
}
