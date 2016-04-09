using UnityEngine;
using System.Collections;

public class LevelTransitionCharacter : MonoBehaviour {

    //Public variables
    public string tagName;

    //Private variables
    private GameObject[] objectsHolder;

	// When object is first created
	void Awake () {

        //Finds any objects with the player tag and then if the size is greater than one then destroys self
        objectsHolder = GameObject.FindGameObjectsWithTag(tagName);
        if(objectsHolder != null && objectsHolder.Length > 1)
        {
            Destroy(gameObject);
        }

        //prevents object from being destroyed when changing levels
        DontDestroyOnLoad(gameObject);
	}
	
}
