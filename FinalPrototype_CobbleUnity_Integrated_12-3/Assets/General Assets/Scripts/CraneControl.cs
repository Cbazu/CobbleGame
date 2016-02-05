using UnityEngine;
using System.Collections;

public class CraneControl : MonoBehaviour {

	bool inRange;
	Transform magnetic;
	Rigidbody magObject;
	public float radius = 0.25f;
	public float force = 10.0f;
	public float speed = 1.0f;

	// Use this for initialization
	void Start () {
		inRange = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (inRange) {
			Vector3 magnetField = magnetic.position - transform.position;
			float index = (radius - magnetField.magnitude) / radius;
			magObject.AddForce(force * magnetField * index);
			//Debug.Log (magnetField.magnitude);
			//magnetic.transform.position = Vector3.MoveTowards(magnetic.transform.position, transform.position, Time.deltaTime * speed);
		}

		Vector3 position =  new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
		transform.position += position * speed * Time.deltaTime;

	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "magnetic") {
			inRange = true;
			other.GetComponent<PatrolTest>().inRange = true;
			magObject = other.attachedRigidbody;
			magnetic = other.GetComponent<Transform> ();
		}
	}

	void OnTriggerExit(Collider other){
		if (other.gameObject.tag == "magnetic")
			inRange = false;
	}

}
