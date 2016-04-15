using UnityEngine;
using System.Collections;

public class CameraZoom : MonoBehaviour {

	public GameObject cameraController;

	// Use this for initialization
	void Start () {
	
		//cameraController.GetComponent<PointAndFollowObject> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other){

		if (other.tag == "Player") {
			//cameraController.GetComponent<PointAndFollowObject> ();
		}
	}
}
