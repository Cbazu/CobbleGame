using UnityEngine;
using System.Collections;

public class ConvBlockSpawner : MonoBehaviour {

	public GameObject convPrefab;
	public float spawnRate = 2.5f;

	// Use this for initialization
	void Start () {
	
		InvokeRepeating ("SpawnPrefab", 0, spawnRate);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void SpawnPrefab(){

		GameObject convBlockClone;
		convBlockClone = Instantiate (convPrefab, transform.position, transform.rotation) as GameObject;
	}
}
