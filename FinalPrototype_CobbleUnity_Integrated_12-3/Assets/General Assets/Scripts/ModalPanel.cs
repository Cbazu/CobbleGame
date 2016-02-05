using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

public class ModalPanel : MonoBehaviour {

	public Text message;
	public Image iconImage;
	public Button button1Button;
	public Button button2Button;
	public Button exitButton;
	public GameObject modalPanelObject;

	private static ModalPanel modalPanel;

	public static ModalPanel Instance () {
		if (!modalPanel) {
			modalPanel = FindObjectOfType(typeof (ModalPanel)) as ModalPanel;
			if (!modalPanel)
				Debug.LogError ("There needs to be one active ModalPanel script on a GameObject in your scene.");
		}
		
		return modalPanel;
	}

	// button event function: A string, event 1, 2, and 3
	public void Choice(string message, UnityAction button1Event, UnityAction button2Event, UnityAction exitEvent){
		modalPanelObject.SetActive (true);
		Time.timeScale = 0; //pause

		button1Button.onClick.RemoveAllListeners();
		button1Button.onClick.AddListener (button1Event);
		//button1Button.onClick.AddListener (ClosePanel);

		button2Button.onClick.RemoveAllListeners();
		button2Button.onClick.AddListener (button2Event);
		//button2Button.onClick.AddListener (ClosePanel);

		exitButton.onClick.RemoveAllListeners();
		exitButton.onClick.AddListener (exitEvent);
		exitButton.onClick.AddListener (ClosePanel);

		this.message.text = message;

		//this.iconImage.gameObject.SetActive (false);

		button1Button.gameObject.SetActive (true);
		button2Button.gameObject.SetActive (true);
		exitButton.gameObject.SetActive (true);
	}

	public void ClosePanel(){
		modalPanelObject.SetActive (false);
		Time.timeScale = 1; //unpause
	}
}
