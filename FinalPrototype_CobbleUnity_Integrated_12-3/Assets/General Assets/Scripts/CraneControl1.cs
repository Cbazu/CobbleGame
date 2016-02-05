using UnityEngine;
using System.Collections;

public class CraneControl1 : MonoBehaviour {

	public float rotateSpeed = -10f;

	// Use this for initialization
	void Start () {
	

	}
	
	// Update is called once per frame
	void FixedUpdate () {

		transform.Rotate (0, Time.deltaTime * rotateSpeed, 0);
		//Debug.Log (transform.localRotation.y);
		if (transform.eulerAngles.y > 25.0f) {
			rotateSpeed = rotateSpeed * -1.0f;
			Debug.Log ("reverse");
		}
	}

}
