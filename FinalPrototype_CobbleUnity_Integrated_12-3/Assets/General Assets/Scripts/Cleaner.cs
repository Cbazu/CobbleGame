using UnityEngine;
using System.Collections;

public class Cleaner : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other){
		Debug.Log (other.name);
		if (other.name == "Droplet(Clone)") {
			Destroy (other.gameObject);
		}

	}
}
