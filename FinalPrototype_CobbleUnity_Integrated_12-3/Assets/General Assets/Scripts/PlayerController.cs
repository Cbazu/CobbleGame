using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float RespawnTime = 1.0f;

	// Use this for initialization
	void Start () {
		GameManager.Notifications.AddListener(this, "Die");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public IEnumerator Die(){
		Debug.Log ("I Died");

		yield return new WaitForSeconds(RespawnTime);
		//Reload the level
		Application.LoadLevel(Application.loadedLevel);

	}
}
