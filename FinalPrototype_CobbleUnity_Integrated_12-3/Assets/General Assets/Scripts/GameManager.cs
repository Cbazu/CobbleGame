//GameManager
//Singleton and persistent object to manage game state
//For high level control over game
//--------------------------------------------------------------
using UnityEngine;
using System.Collections;
//Game Manager requires other manager components
[RequireComponent (typeof (NotificationsManager))] //Component for sending and receiving notifications
//--------------------------------------------------------------
public class GameManager : MonoBehaviour
{
	//--------------------------------------------------------------
	//public properties
	//C# property to retrieve currently active instance of object, if any
	public static GameManager Instance
	{
		get
		{
			if (instance == null) instance = new GameObject ("GameManager").AddComponent<GameManager>(); //create game manager object if required
			return instance;
		}
	}
	//--------------------------------------------------------------
	//C# property to retrieve notifications manager
	public static NotificationsManager Notifications
	{
		get
		{
			if(notifications == null) notifications =  instance.GetComponent<NotificationsManager>();
			return notifications;
		}
	}
	//--------------------------------------------------------------
	//C# property to retrieve and set input allowed status
	public bool InputAllowed
	{
		get{return bInputAllowed;}
		
		set
		{
			//Set Input
			bInputAllowed = value;
			
			//Post notification about input status changed
			Notifications.PostNotification(this, "InputChanged");
		}
	}

	//Save/Load manager
/*	public static LoadSaveManager StateManager
	{
		get{
			if (statemanager == null) {
				statemanager = instance.GetComponent<LoadSaveManager> ();
			}
			return statemanager;
		}

	}
*/
	//--------------------------------------------------------------
	//Private variables
	//--------------------------------------------------------------
	//Internal reference to single active instance of object - for singleton behaviour
	private static GameManager instance = null;

	//Internal reference to notifications object
	private static NotificationsManager notifications = null;

//	private static LoadSaveManager statemanager = null;

	private static bool bShouldLoad = false;

	//Can game accept user input?
	private bool bInputAllowed = true;
	//--------------------------------------------------------------
	// Called before Start on object creation
	void Awake ()
	{	
		//Check if there is an existing instance of this object
		if((instance) && (instance.GetInstanceID() != GetInstanceID()))
			DestroyImmediate(gameObject); //Delete duplicate
		else
		{
			instance = this; //Make this object the only instance
			DontDestroyOnLoad (gameObject); //Set as do not destroy
		}
	}
	//--------------------------------------------------------------
	// Use this for initialization
	void Start () 
	{
		//Add game menu listeners
		Notifications.AddListener (this, "RestartGame");
		Notifications.AddListener (this, "ExitGame");
		Notifications.AddListener (this, "SaveGame");
		Notifications.AddListener (this, "LoadGame");

		//if need to load level
/*		if (bShouldLoad) {
			StateManager.Load (Application.persistentDataPath + "/SaveGame.xml");
			bShouldLoad = false;
		}
*/
	}
	//--------------------------------------------------------------
	//Pause/Exit Game to menu
	void Update()
	{
/*		if (Input.GetKeyDown (KeyCode.Q)) 
		{
			//Load Menu
			Application.LoadLevel (0);
		} 
*/
	}
	//--------------------------------------------------------------
	//Restart Game
	public void RestartGame()
	{
		//Load first level
		Application.LoadLevel(0);
		bInputAllowed = true;
	}
	//--------------------------------------------------------------
	//Exit Game
	public void ExitGame()
	{
		Application.Quit();
	}
	//--------------------------------------------------------------
/*	public void SaveGame()
	{
		StateManager.Save (Application.persistentDataPath + "/SaveGame.xml");
	}
*/
	public void LoadGame()
	{
		//set on restart
		bShouldLoad = true;

		//restart
		RestartGame ();
	}
}
