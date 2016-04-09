using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float RespawnTime = 1.0f;

    //Private variables
    private string targetDestination;
    private float targetOffset;
    private bool isReadyToTeleport = false;

    void Awake()
    {
        Debug.Log("Cobble name is " + gameObject.name + " and ID " + gameObject.GetInstanceID());
    }

	// Use this for initialization
	void Start () {
		GameManager.Notifications.AddListener(this, "Die");
        Debug.Log("Cobble name is " + gameObject.name + " and ID " + gameObject.GetInstanceID());
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

    public void SetTeleportTarget(string destination, float yOffset)
    {

        targetDestination = destination;
        targetOffset = yOffset;
        isReadyToTeleport = true;
        Debug.Log("Target Destination: " + targetDestination + " Offset: " + targetOffset);

    }

    private void TeleportToDestination()
    {

        //variables
        GameObject[] teleportTargets;

        //read in targets
        teleportTargets = GameObject.FindGameObjectsWithTag("TeleportTarget");
        Debug.Log("Teleport to Destination has been called.");

        if(teleportTargets == null)
        {
            Debug.Log("teleport target not found");
            return;
        }
        else
        {
            for(int i=0;i < teleportTargets.Length;i++)
            {
                if(teleportTargets[i].gameObject.GetComponent<MarkerAttributes>().markerName == targetDestination)
                {

                    gameObject.transform.position = teleportTargets[i].transform.position + new Vector3(0, targetOffset, 0);
                    gameObject.transform.rotation = teleportTargets[i].transform.rotation;
                    i = teleportTargets.Length; //exits loop
                    Debug.Log("Teleport Target Found");
                }
            }
        }
        //check to see if any of the targets match
        

    }

    void OnLevelWasLoaded(int level)
    {
        Debug.Log("Player Controller level loaded and isLevelReadyToLoad: "+isReadyToTeleport);
        if (isReadyToTeleport)
        {
            TeleportToDestination();
        }
    }

    void OnDestroy()
    {
        Debug.Log("Cobble name: " + gameObject.name + " ID: " + gameObject.GetInstanceID() + " has been destroyed");
    }
}
