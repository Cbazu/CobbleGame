using UnityEngine;
using System.Collections;

public class New_PlatformControl : MonoBehaviour {
	
	Vector3 pointA = new Vector3();
	public GameObject pointB;
	//Rigidbody platformRB;
	public float thrust = 0.25f;
	GameObject platform;

	// Use this for initialization
	void Start () {
		//platformRB = GetComponent<Rigidbody>();
		platform = gameObject;
		pointA = transform.position;
	}

	
	// Update is called once per frame
	void Update () {
		moveObject();
		//platformRB.AddForce (new Vector3 (point1.transform.position.x, point1.transform.position.y) * thrust);
	}

	void moveObject(){
			float i = Mathf.PingPong(Time.time * thrust, 1.0f);
			transform.position = Vector3.Lerp(pointA, pointB.transform.position, i);
		
	}
	
}
