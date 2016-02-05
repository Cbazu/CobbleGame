using UnityEngine;
using System.Collections;

public class DropletSpawner : MonoBehaviour {

	public GameObject dropPrefab;
	public float spawnRate = 3f;

	// Use this for initialization
	void Start () {
		InvokeRepeating ("Spawn", 0.01f, spawnRate);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void Spawn(){
		GameObject instance = Instantiate (dropPrefab, transform.position, transform.rotation) as GameObject;
	}
}
