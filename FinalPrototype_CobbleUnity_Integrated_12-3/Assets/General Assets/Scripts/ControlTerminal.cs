using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

public class ControlTerminal : MonoBehaviour {

	public bool isPowered = false;
	public GameObject controlObject;
	public string promptText;
	public Text button1Text;
	public string button1Name;
	public Text button2Text;
	public string button2Name;


	private ModalPanel modalPanel;
	private UnityAction myButton1Action;
	private UnityAction myButton2Action;
	private UnityAction myExitAction;
	private bool isActive = false;
	private float coolDownTime = 3.0f;


	// Use this for initialization
	void Awake () {
		modalPanel = ModalPanel.Instance ();
		
		myButton1Action = new UnityAction (Button1Function);
		myButton2Action = new UnityAction (Button2Function);
		myExitAction = new UnityAction (ExitFunction);
	}

	void FixedUpdate(){
		if (isActive == true && coolDownTime >= 0f) {
			coolDownTime -= Time.deltaTime;
		} else {
			isActive = false;
			coolDownTime = 3.0f;
		}
	}
	
	public void Prompt(){
		isActive = true;
		button1Text.text = button1Name;
		button2Text.text = button2Name;

		modalPanel.Choice (promptText, Button1Function, Button2Function, ExitFunction);
	}

	void Button1Function(){
		controlObject.GetComponent<Gate> ().Activate ();
		modalPanel.ClosePanel ();
	}

	void Button2Function(){
		modalPanel.ClosePanel ();
	}

	void ExitFunction(){

	}

	void OnTriggerEnter(Collider other){
		if (other.tag == "Player" && isPowered == true && isActive == false) {
			Prompt ();
		}
	}

	void OnTriggerExit(Collider other){
		if (other.tag == "Player") {
			modalPanel.ClosePanel ();
		}
	}
}
