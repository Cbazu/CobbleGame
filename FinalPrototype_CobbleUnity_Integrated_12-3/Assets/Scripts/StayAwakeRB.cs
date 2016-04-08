using UnityEngine;
using System.Collections;

public class StayAwakeRB : MonoBehaviour {

    public bool stayAwake = true;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
           if(stayAwake)
        {
            GetComponent<Rigidbody>().WakeUp(); //Keeps Rigidbody awake when not moving
        }
	}
}
