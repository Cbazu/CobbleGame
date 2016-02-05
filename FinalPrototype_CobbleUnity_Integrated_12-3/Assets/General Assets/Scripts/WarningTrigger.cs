using UnityEngine;
using System.Collections;

public class WarningTrigger : MonoBehaviour {

	public GameObject enemyBomb;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other){
		if (other.tag == "Player") {
			enemyBomb.GetComponent<Enemy_BombAI>().warning = true;
		}
	}

	void OnTriggerExit(Collider other){
		if (other.tag == "Player") {
			enemyBomb.GetComponent<Enemy_BombAI>().warning = false;
		}
	}
}
