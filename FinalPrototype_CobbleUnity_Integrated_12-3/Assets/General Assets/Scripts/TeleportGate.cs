using UnityEngine;
using System.Collections;

public class TeleportGate : MonoBehaviour {
	public string destination;
	bool isDisplaying = false;
	//public GUISkin InstructionBoxSkin;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown (KeyCode.T) && isDisplaying ){
			Application.LoadLevel(destination);
		}
	}

	void OnGUI ()
	{
		//This is where we draw a box telling the Player how to pick up the item.
		//GUI.skin = InstructionBoxSkin;
		//GUI.color = Color(1f, 1f, 1f, 0.7f);
		if(isDisplaying)
			GUI.Box(new Rect ((Screen.width/2)-100, (Screen.height/2)-100,200,30), "Press T to Travel to " + destination + ".");
	}

	void OnTriggerEnter(Collider other){
		if (other.tag == "Player") {
			isDisplaying = true;

		}
	}
	void OnTriggerExit(Collider other){
		if (other.tag == "Player") {
			isDisplaying = false;

		}
	}
}
