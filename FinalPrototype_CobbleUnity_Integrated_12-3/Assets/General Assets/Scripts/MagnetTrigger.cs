using UnityEngine;
using System.Collections;

public class MagnetTrigger : MonoBehaviour {

	public CraneControl1 craneScript;
	public GameObject magnet;
	GameObject colObject;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other){
		colObject = other.gameObject;
		craneScript.TriggerEntered (magnet, colObject);
	}
}
