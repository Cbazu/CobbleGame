using UnityEngine;
using System.Collections;

public class CutLaser : MonoBehaviour {

	public GameObject objectToCut;
	public GameObject CuttingLaser;
	public GameObject CuttingLaserStart;
	public GameObject CuttingLaserEnd;
	public Canvas promptUI;
	private bool inRange = false;

	// Use this for initialization
	void Start () {
		CuttingLaserStart = GameObject.Find ("LaserStart");
		CuttingLaserEnd = GameObject.Find ("LaserEnd");
	}
	
	// Update is called once per frame
	void Update () {

		if (inRange == true && Input.GetButton ("Fire1")) {
			//CuttingLaser.GetComponent<Renderer> ().enabled = true;
			CuttingLaser.SetActive (true);
			CuttingLaserEnd.transform.position = objectToCut.transform.position;
			CuttingLaserEnd.transform.LookAt (CuttingLaserStart.transform);
		} else {
			CuttingLaser.SetActive (false);
		}

	}
		

	void OnTriggerEnter(Collider other){

		if (other.tag == "Player") {
			promptUI.GetComponent<Canvas> ().enabled = true;
			inRange = true;
		}
	}

	void OnTriggerExit(Collider other){

		if (other.tag == "Player") {
			promptUI.GetComponent<Canvas> ().enabled = false;
			inRange = false;
		}
	}
}
