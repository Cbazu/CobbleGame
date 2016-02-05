using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class Dropdown : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

	public RectTransform container;
	public bool isOpen;

	// Use this for initialization
	void Start () {
		container = transform.FindChild ("Container").GetComponent<RectTransform>();
		isOpen = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (isOpen) {
			Vector3 scale = container.localScale;
			scale.y = Mathf.Lerp (scale.y, 1, Time.deltaTime * 12);
			container.localScale = scale;
		} else {
			Vector3 scale = container.localScale;
			scale.y = Mathf.Lerp(scale.y, 0, Time.deltaTime * 12);
			container.localScale = scale;
		}
	}

	public void OnPointerEnter(PointerEventData evenData){
		isOpen = true;
	}

	public void OnPointerExit(PointerEventData evenData){
		isOpen = false;
	}
}
