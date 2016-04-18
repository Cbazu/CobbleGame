using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TeleportGate : MonoBehaviour
{

    //public variables
    public string destinationLevel;
    public string destinationTarget;
    public GameObject loadingUI;
    public Text loadTextDisplay;
    public Image loadBarImage;
    public Image loadBarBG;
    public float markerYDifferential;
    public float loadWaitTime = 5f;
	public GameObject promptCanvas = null;


    //private variables
    private bool isDisplaying = false;
    private float maxPeriodDisplayTime = .5f; //Max Time for loading period text to display
    private float periodDisplayTime = 0.0f;
    private AsyncOperation levelLoading = null; //When assigned a load is in progress
    private string placeholderText = "Loading";
    private int periodCounter = 0;
    private float maxLoadBarXPixel = 355.0f;
    private float maxLoadBarYPixel = 39.7f;
    private GameObject playerHolder;
    private float levelProgress = 0f;
    private bool isLevelLoading = false;

    //testing variables
    private float testTime = 0.0f;
    private float maxTestTime = 3.0f;
    private bool testing = false;



    void Awake()
    {
        if (gameObject.transform.parent != null)
        {
            DontDestroyOnLoad(gameObject.transform.parent.gameObject);
            Debug.Log("Has parent");
            Debug.Log("Object name is " + gameObject.transform.parent.gameObject.name + " and id is " + gameObject.transform.parent.gameObject.GetInstanceID());
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            Debug.Log("Does not have parent");
            Debug.Log("Object name is " + gameObject.name + " and id is " + gameObject.GetInstanceID());
        }
        
    }

    // Use this for initialization
    void Start()
    {
        loadBarImage = loadingUI.GetComponentInChildren<Image>().GetComponentInChildren<Image>();
    }

    // Update is called once per frame
    void Update()
    {

        //Initiates loading screen and loading of the next level. Conditional only fires
        //when player presses the correct key, GUI instructions are displaying, and the next level has not already
        //started loading
       // if (Input.GetKeyDown(KeyCode.T) && isDisplaying && (!testing /*levelLoading == null*/))
		if (Input.GetButtonDown("Interact") && isDisplaying && (!testing /*levelLoading == null*/))
        {
            LoadNextLevel();
        }

        //uses a set time before transitioning for testing purposes
        if (testing)
        {
            if (periodDisplayTime < maxPeriodDisplayTime) periodDisplayTime += Time.deltaTime;
            else
            {
                periodDisplayTime = 0.0f;
                periodCounter = (periodCounter + 1) % 3;
                placeholderText = "Loading";
                for (int i = 0; i < periodCounter; i++)
                {
                    placeholderText += ".";
                }
            }
            loadTextDisplay.text = placeholderText;
            loadBarImage.rectTransform.localScale = new Vector3((levelProgress * maxLoadBarXPixel), 1, 1); //new Vector2((levelLoading.progress * maxLoadBarXPixel), maxLoadBarYPixel);
            Debug.Log("Progress is " + levelProgress * 100 + "%");
        }
        /*else if (testTime >= maxTestTime)
        {
            levelLoading = Application.LoadLevelAsync(destinationLevel);
            levelLoading.allowSceneActivation = true;
            Debug.Log("New level has been loaded.");
            if (levelLoading.isDone)
            {
                Debug.Log("Level is loaded.");
                MovePlayerToTarget();
                Destroy(gameObject); // destroy object after player has been moved
            }
        }
        else
        {
            testTime += Time.deltaTime;
        }*/

    }



    //Moves the player to the target designated at design time and reactivates it
    void MovePlayerToTarget()
    {

        Debug.Log("Move Player has been called");

        //variables
        GameObject[] teleportTarget;

        //finds the teleport target with the designated name and moves player to it
        teleportTarget = GameObject.FindGameObjectsWithTag("TeleportTarget");

        if (teleportTarget == null)
        {
            Debug.Log("Teleport target not set!");
        }
        else
        {
            for (int i = 0; i < teleportTarget.Length; i++)
            {

                bool isTargetValid = false;

                //goes through all teleport targets in the level and finds a matching target then moves player to coordinates of target
                if (teleportTarget[i].GetComponent<MarkerAttributes>().markerName == destinationLevel)
                {
                    playerHolder.transform.position = teleportTarget[i].transform.position + new Vector3(0, markerYDifferential, 0);    //moves player to target position       
                    playerHolder.transform.rotation = teleportTarget[i].transform.rotation; //player assumes rotation of teleport marker
                    i = teleportTarget.Length; // exit loop
                    Debug.Log("Teleport target is set");
                    isTargetValid = true;
                }
                Debug.Log("Teleport target " + (i + 1) + " is valid: " + isTargetValid);
            }
        }

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            isDisplaying = true;
            playerHolder = other.gameObject;
			promptCanvas.GetComponent<Canvas> ().enabled = true;
			promptCanvas.GetComponentInChildren<Text> ().text = "Travel to " + destinationLevel;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            isDisplaying = false;
            playerHolder = null;
			promptCanvas.GetComponent<Canvas> ().enabled = false;
        }
    }


    void LoadNextLevel()
    {
        //Starts loading the level input during design time
        testing = true;
        Debug.Log("Load Next Level called.");
        loadingUI.SetActive(true);
        playerHolder.GetComponent<PlayerController>().SetTeleportTarget(destinationTarget, markerYDifferential);
        StartCoroutine(LoadNewScene(loadWaitTime));
        isLevelLoading = true;
        Debug.Log("Teleport Gate isLevelLoading: " + isLevelLoading);
        
    }

//    void OnGUI()
//    {
//        if (isDisplaying == true)
//        {
//            GUI.Label(new Rect(Screen.width / 2, Screen.height / 2, 100, 50), "Press T to travel to " + destinationLevel);
//        }
//    }


    IEnumerator LoadNewScene(float levelWait)
    {

        // This line waits for 3 seconds before executing the next line in the coroutine.
        // This line is only necessary for this demo. The scenes are so simple that they load too fast to read the "Loading..." text.
        yield return new WaitForSeconds(levelWait);

        // Start an asynchronous operation to load the scene that was passed to the LoadNewScene coroutine.
        AsyncOperation async = Application.LoadLevelAsync(destinationLevel);

        //Sets the flag for checking what to do on level load
        Debug.Log("Is Level loading?" + isLevelLoading);

        // While the asynchronous operation to load the new scene is not yet complete, continue waiting until it's done.
        while (!async.isDone)
        {
            levelProgress = async.progress;
            yield return null;
        }

    }

    void OnLevelWasLoaded(int level)
    {
        Debug.Log("Level loaded was called and isLevelLoading is " + isLevelLoading);
        if (isLevelLoading)
        {
            MovePlayerToTarget();
            Destroy(gameObject);
        }
    }
}
