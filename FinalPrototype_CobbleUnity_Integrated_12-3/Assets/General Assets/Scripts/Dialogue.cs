using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

public class Dialogue : MonoBehaviour {

	public int beginDialogueLine;
	public int endDialogeLine;
	public bool useButton1;
	public Text button1Text;
	public string button1Name;
	public bool useButton2;
	public Text button2Text;
	public string button2Name;
	public TextAsset textFile;
	public string[] textLines;
	public int currentLine;
	public bool hasBeenTriggered = false;

	private ModalPanel modalPanel;
	private UnityAction myButton1Action;
	private UnityAction myButton2Action;
	private UnityAction myExitAction;

	void Awake () {
		modalPanel = ModalPanel.Instance ();
		
		myButton1Action = new UnityAction (TestButton1Function);
		myButton2Action = new UnityAction (TestButton2Function);
		myExitAction = new UnityAction (TestExitFunction);

		//read in dialogue from text file into string array
		if (textFile != null) {
			textLines = (textFile.text.Split ('\n'));
		}

		currentLine = beginDialogueLine;
	}


	//  Send to the Modal Panel to set up the Buttons and Functions to call
	public void TestButtons () {
		button1Text.text = button1Name;
		button2Text.text = button2Name;

/*		if (!useButton1)
			modalPanel.button1Button.gameObject.SetActive (false);
		if (!useButton2)
			modalPanel.button2Button.gameObject.SetActive (false);
*/
		modalPanel.Choice (textLines[currentLine], TestButton1Function, TestButton2Function, TestExitFunction);

	}
	
	//  These are wrapped into UnityActions
	void TestButton1Function () {

	}

	//next function
	void TestButton2Function () {
		if (currentLine < endDialogeLine) {
			currentLine += 1;
		}
		modalPanel.Choice (textLines[currentLine], TestButton1Function, TestButton2Function, TestExitFunction);

	}
	
	void TestExitFunction () {
		modalPanel.ClosePanel ();
	}

	void OnTriggerEnter(Collider other){
		if (other.tag == "Player" && hasBeenTriggered == false) {
			TestButtons ();
			hasBeenTriggered = true;
		}
	}

	void OnTriggerExit(Collider other){
		if (other.tag == "Player")
			currentLine = beginDialogueLine;
	}
}