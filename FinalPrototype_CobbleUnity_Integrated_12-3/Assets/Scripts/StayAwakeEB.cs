using UnityEngine;
using System.Collections;

public class StayAwakeEB : MonoBehaviour {

    public bool stayAwake = true;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
           if(stayAwake)
        {
            GetComponent<Rigidbody>().WakeUp();
        }
	}
}
