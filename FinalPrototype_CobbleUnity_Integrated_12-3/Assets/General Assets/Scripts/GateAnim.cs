using UnityEngine;
using System.Collections;

public class GateAnim : MonoBehaviour {

	public Animator animator;
	bool isOpen = false;

	// Use this for initialization
	void Start () {
	
		animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
	
		if (isOpen == true) {
			animator.SetBool ("isOpen", true);
		} else {
			animator.SetBool ("isOpen", false);
		}

	}

	public void Activate(){
		isOpen = !isOpen;
	}
}
