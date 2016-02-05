using UnityEngine;
using System.Collections;

public class HoldCharacter : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider col){
		col.transform.parent = gameObject.transform;
	}

	void OnTriggerExit(Collider col){
		col.transform.parent = null;
	}
}
