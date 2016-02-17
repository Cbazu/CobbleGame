using UnityEngine;
using System.Collections;

public class PlateAnimate : MonoBehaviour {

	public Animator animator;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other){
		animator.SetBool ("isDown", true);
	}

	void OnTriggerExit(Collider other){
		animator.SetBool ("isDown", false);
	}
}
