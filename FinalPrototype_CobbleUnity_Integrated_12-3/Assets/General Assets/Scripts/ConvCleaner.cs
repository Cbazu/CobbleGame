using UnityEngine;
using System.Collections;

public class ConvCleaner : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

/*	void OnTriggerEnter(Collider other){
		if (other.tag == "Conveyor") {
			Destroy (other.gameObject);
		}
	}
*/

	void OnCollisionEnter(Collision other){
		Destroy (other.gameObject);
	}
}
