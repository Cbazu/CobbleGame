using UnityEngine;
using System.Collections;

public class AcidPool : MonoBehaviour {

	private AudioSource source;
	public AudioClip AcidBubble;

	// Use this for initialization
	void Start () {
		source = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other){
		if (other.tag == "Player") {
			source.PlayOneShot (AcidBubble, 1f);
			GameManager.Notifications.PostNotification(this, "Die");
		}
	}
}
