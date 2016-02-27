using UnityEngine;
using System.Collections;

public class PlateAnimate : MonoBehaviour {

	public Animator animator;
	public GameObject controlObject;
	private bool plateDown = false;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();

	}
	
	// Update is called once per frame
	void FixedUpdate () {
	/*
		if (plateDown == true) {
			controlObject.GetComponent<Animator> ().SetBool ("isOpen", true);
		}
		if (plateDown == false) {
			controlObject.GetComponent<Animator> ().SetBool ("isOpen", false);
		}
	*/
	}

	void OnTriggerEnter(Collider other){
		animator.SetBool ("isDown", true);
		plateDown = true;

		controlObject.GetComponent<GateAnim> ().Activate ();
		//controlObject.GetComponent<Animator> ().SetBool ("isOpen", true);
	}

	void OnTriggerExit(Collider other){
		//if (plateDown == false) {
			animator.SetBool ("isDown", false);
			controlObject.GetComponent<GateAnim> ().Activate ();
			//controlObject.GetComponent<Animator> ().SetBool ("isOpen", false);
		//}
		plateDown = false;
	}

	void OnTriggerStay(Collider other){
		animator.SetBool ("isDown", true);
		if (other.tag == "IntObject") {
			controlObject.GetComponent<Animator> ().SetBool ("isOpen", true);
			plateDown = true;
		}
	}

}
