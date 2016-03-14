using UnityEngine;
using System.Collections;

public class ConvBeltAnim : MonoBehaviour {

	public float scrollSpeed = 1.1F;
	public Renderer rend;
	public bool isMoving = true;

	void Start() {
		rend = GetComponent<Renderer>();
	}
	void Update() {

		if (isMoving) {
			float offset = Time.time * scrollSpeed;
			rend.material.SetTextureOffset ("_MainTex", new Vector2 (0, offset));
			if(offset >= 10f){
				offset = 0f;
			}
		}
	}
}
