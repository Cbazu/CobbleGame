using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TeleportGate : MonoBehaviour {

	//public variables
	public string destinationLevel;
    public string destinationTarget;
	public GameObject loadingUI;
	public Text loadTextDisplay;
	public Image loadBarImage;
	//public Image loadBarBG;

	//private variables
	private bool isDisplaying = false;
	private float maxPeriodDisplayTime = .5f; //Max Time for loading period text to display
	private float periodDisplayTime = 0.0f;
	private AsyncOperation levelLoading = null; //When assigned a load is in progress
	private string placeholderText = "Loading";
	private int periodCounter = 0;
	private float maxLoadBarXPixel = 355.0f;
	private float maxLoadBarYPixel = 39.7f;

	//testing variables
	private float testTime = 0.0f;
	private float maxTestTime = 3.0f;
	private bool testing = false;


	// Use this for initialization
	void Start () {
        //loadBarImage = loadingUI.GetComponentInChildren<Image>().GetComponentInChildren<Image>();
	}
	
	// Update is called once per frame
	void Update () {

		//Initiates loading screen and loading of the next level. Conditional only fires
		//when player presses the correct key, GUI instructions are displaying, and the next level has not already
		//started loading
		if(Input.GetKeyDown (KeyCode.T) && isDisplaying && (!testing /*levelLoading == null*/)){
			LoadNextLevel ();
		}
		if (testing) {
			if (levelLoading != null) {
			}
			if ((testTime >= maxTestTime) && (levelLoading == null)) {
				levelLoading = Application.LoadLevelAsync (destinationLevel);
			} else {
				testTime += Time.deltaTime;
			}

			if ((testTime >= maxTestTime) && (testing)) {
				levelLoading = Application.LoadLevelAsync (destinationLevel);
			} else if (testing) {
				testTime += Time.deltaTime;
			}
			if(periodDisplayTime < maxPeriodDisplayTime) periodDisplayTime += Time.deltaTime;
			else {
				periodDisplayTime = 0.0f;
				periodCounter = (periodCounter + 1) % 3;
				placeholderText = "Loading";
				for(int i = 0;i < periodCounter;i++)
				{
					placeholderText +=".";
				}
			}
			loadTextDisplay.text=placeholderText;
            loadBarImage.rectTransform.localScale = new Vector3((levelLoading.progress * maxLoadBarXPixel), 1, 1); //new Vector2((levelLoading.progress * maxLoadBarXPixel), maxLoadBarYPixel);
		}
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


	void LoadNextLevel()
	{
		//Starts loading the level input during design time
		testing = true;
		Debug.Log ("Load Next Level called.");
		loadingUI.SetActive(true);
	}

	void OnGUI() {
		if (isDisplaying == true){
			GUI.Label (new Rect (Screen.width / 2, Screen.height / 2, 100, 50), "Press T to travel to " + destinationLevel);
		}
	}
}
