using UnityEngine;
using System.Collections;

public class Gate : MonoBehaviour {
	
	public bool isOpen = false;
	public Transform gateDoor;
	public Transform closePosition;
	public Transform openPosition;
	public Vector3 newPosition;
	public float smooth;
	public float moveTime;

	private bool isMoving = false;
	private float initTime = 3.5f;

	// Use this for initialization
	void Start () {
		initTime = moveTime;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (isOpen == false && moveTime >= 0f) {
			newPosition = openPosition.position;
			gateDoor.position = Vector3.Lerp (gateDoor.position, newPosition, smooth * Time.deltaTime);
			moveTime -= Time.deltaTime;

		} else if (isOpen && moveTime >= 0f) {
			newPosition = closePosition.position;
			gateDoor.position = Vector3.Lerp (gateDoor.position, newPosition, smooth * Time.deltaTime);
			moveTime -= Time.deltaTime;
		}
	}

	public void Activate(){
		isOpen = !isOpen;
		moveTime = initTime;
	}
}
