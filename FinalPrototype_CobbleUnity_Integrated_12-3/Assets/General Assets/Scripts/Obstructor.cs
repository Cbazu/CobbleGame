using UnityEngine;
using System.Collections;

public class Obstructor : MonoBehaviour {

	public Color transparentColor;
	private Color initialColor;
	public GameObject obsObject;
	public float fadePerSecond = 10f;
		
	// Use this for initialization
	void Start () {
		obsObject = gameObject;
		initialColor = GetComponent<Renderer> ().material.color;
	}


	
	public void SetTransparent(){

		obsObject.GetComponent<Renderer> ().material.color = transparentColor;
		//Color color = GetComponent<Renderer> ().material.color;
		//obsObject.GetComponent<Renderer> ().material.color = new Color(color.r, color.g, color.b, color.a - (fadePerSecond * Time.deltaTime));
	}

	public void SetToNormal(){
		obsObject.GetComponent<Renderer> ().material.color = initialColor;
	}
}
