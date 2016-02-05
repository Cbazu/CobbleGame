using UnityEngine;
using System.Collections;

public class PatrolTest : MonoBehaviour {

	public float thrust;
	public Rigidbody rb;
	public bool inRange = false;
	void Start() {
		rb = GetComponent<Rigidbody>();
	}
	void Update() {
		if(inRange == false)
			rb.AddForce(transform.forward * thrust);
	}


}
