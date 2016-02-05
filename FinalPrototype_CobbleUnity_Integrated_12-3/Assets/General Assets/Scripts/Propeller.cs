using UnityEngine;
using System.Collections;

public class Propeller : MonoBehaviour {
		
	public float speed = 500f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		transform.Rotate (0, 0, Time.deltaTime * speed);
	}
}
