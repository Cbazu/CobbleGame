using UnityEngine;
using System.Collections;

public class BatterySocket : MonoBehaviour {

	public GameObject objectToPower;
	public AudioClip powerUpSound;

	private bool isTerminal = false;
	private AudioSource soundSource;
	ControlTerminal terminal;

	// Use this for initialization
	void Start () {

		if(objectToPower.name.Contains ("Terminal")){
			terminal = objectToPower.GetComponent<ControlTerminal>();
			isTerminal = true;
		}
	
		soundSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void PowerOn(){
		if (isTerminal) {
			terminal.isPowered = true;
		}
	}

	void OnTriggerEnter(Collider other){
		if (other.name.Contains ("Battery")) {
			PowerOn ();
			//Debug.Log ("Battery detected.");
			other.transform.position = this.gameObject.transform.position;
			other.transform.SetParent (this.gameObject.transform);
			soundSource.PlayOneShot (powerUpSound,1);
		}
	}

	
}
