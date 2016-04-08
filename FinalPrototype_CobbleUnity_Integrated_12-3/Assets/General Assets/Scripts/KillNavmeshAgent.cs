﻿using UnityEngine;
using System.Collections;

public class KillNavmeshAgent : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other){
		if (other.tag == "Enemy") {
			other.GetComponent<NavMeshAgent> ().enabled = false;
			other.GetComponent<Rigidbody>().isKinematic = false;
		}
	}
}
