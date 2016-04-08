using UnityEngine;
using System.Collections;

public class Crusher : MonoBehaviour {

	private AudioSource source;
	public AudioClip Crunch;
	public AudioClip Explosion;
	private GameObject exploder = null;
	//public Animator anim;

		// Use this for initialization
	void Start () {
		source = GetComponent<AudioSource> ();
	}

	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter(Collider other){
	if (other.tag == "Player") {
		source.PlayOneShot (Crunch, 1f);
		GameManager.Notifications.PostNotification (this, "Die");
	} else if (other.tag == "Magnetic") {
		source.PlayOneShot (Explosion, 1f);
		//exploder = other.GetComponent<GameObject> ();
		Destroy(other.gameObject);
		Explode ();
	}
	}

void Explode(){
		//Destroy (exploder);
		//anim.SetBool ("Explode", true);
		Debug.Log("Boom!");
}
}
