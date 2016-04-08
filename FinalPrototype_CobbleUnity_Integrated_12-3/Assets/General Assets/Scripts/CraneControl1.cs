using UnityEngine;
using System.Collections;

public class CraneControl1 : MonoBehaviour {

	public float rotateSpeed = -10f;
	public float rotationAngle;
	public Animator anim;
	bool rev = false;
	bool pickUp = false;
	bool letGo = false;
	public GameObject magnet;
	GameObject magObject;
	private float startTime;

	public float initRotation;

	int runStateHash = Animator.StringToHash ("Short Up");

	// Use this for initialization
	void Start () {

		//anim = GetComponentInChildren<Animator> ();
		anim.SetBool ("isReverse", false);

		initRotation = transform.eulerAngles.y;

		rotationAngle += initRotation;
		if (rotationAngle > 360f) {
			rotationAngle -= 360f;
		}


	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
		AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo (0);

		//controls crane rotation and animation states
		if (stateInfo.IsName("Rotate") == true) {

			//Debug.Log ("Euler: " + transform.eulerAngles.y + " rotationAngle: " + rotationAngle);

			transform.Rotate (0, Time.deltaTime * rotateSpeed, 0);
			//Debug.Log (transform.localRotation.y);

			//reverse rotation when at specified angle
			if (transform.eulerAngles.y > rotationAngle) {
				rotateSpeed = rotateSpeed * -1.0f;

				if(transform.eulerAngles.y < 350f){
					anim.SetTrigger ("rev");
					anim.SetBool ("isReverse", true);
					rev = true;
					//letGo = true;
					StartCoroutine (Wait ());
				}

//				if(transform.eulerAngles.y > 350f && rev == true){
//					anim.SetTrigger ("forw");
//				}
				//Debug.Log ("reverse");
			}

		}
		//rotate clockwise
		if(transform.eulerAngles.y > 350f && rev == true){
			anim.SetTrigger ("forw");

		}

		if(stateInfo.IsTag ("forward")){
			anim.SetBool ("isReverse", false);
			rev = false;

		}
		//Debug.Log (stateInfo);
		//------------------------------------------------------------------

		if (pickUp == true) {
			magObject.transform.position = Vector3.Lerp (magObject.transform.position, magnet.transform.position, (Time.time - startTime) / 2f);
		}
		if (letGo == true) {
			pickUp = false;
			//magObject.transform.SetParent (null);
		}


	}

	public void TriggerEntered(GameObject magnetCol, GameObject otherCol){
		if (otherCol.tag == "Player" || otherCol.tag == "Magnetic") {
			pickUp = true;
			letGo = false;
			magObject = otherCol.gameObject;
			startTime = Time.time;
			
		}
	}

	IEnumerator Wait(){
		//Debug.Log ("Waiting");
		yield return new WaitForSeconds (1.5f);
		letGo = true;
	}
}
