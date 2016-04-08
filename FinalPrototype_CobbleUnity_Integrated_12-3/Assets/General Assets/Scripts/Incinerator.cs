using UnityEngine;
using System.Collections;

public class Incinerator : MonoBehaviour {

	public float waitTime;
	public Animator anim;
	public GameObject fire = null;
	public GameObject trigger;

	private float initTime;
	bool isOpen = false;


	// Use this for initialization
	void Start () {
		initTime = waitTime;
	}
	
	// Update is called once per frame
	void Update () {
		if (waitTime < 0f) {
			Activate ();
			waitTime = initTime;
		}
		if (waitTime > 0f) {
			waitTime -= Time.deltaTime;
//			Activate ();
//			waitTime = initTime;
		}
	}

	void Activate(){
		isOpen = !isOpen;
		if (isOpen) {
			anim.SetBool ("isOpen", true);
			fire.GetComponent<ParticleSystem> ().gameObject.SetActive(true);
			trigger.GetComponent<BoxCollider> ().enabled = true;
			//fire.gameObject.SetActive(true);
		} else if (!isOpen) {
			anim.SetBool ("isOpen", false);
			fire.GetComponent<ParticleSystem> ().gameObject.SetActive(false);
			trigger.GetComponent<BoxCollider> ().enabled = false;
			//trigger.SetActive (false);
			//fire.gameObject.SetActive(false);
		}
	}
}
