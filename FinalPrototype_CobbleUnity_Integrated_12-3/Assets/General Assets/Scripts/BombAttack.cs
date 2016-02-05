using UnityEngine;
using System.Collections;

public class BombAttack : MonoBehaviour {

	public GameObject enemyBomb;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other){
		if (other.tag == "Player") {
			enemyBomb.GetComponent<Enemy_BombAI>().isPatrolling = false;
			enemyBomb.GetComponent<Enemy_BombAI>().isAttacking = true;
		}
	}
}
