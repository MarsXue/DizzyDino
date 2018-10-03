using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BoldOnHoverScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

	public Font enterFont;
	private Font exitFont;

	private Text text;

	// Use this for initialization
	void Start () {
		
		text = gameObject.GetComponentInChildren<Text>();
		exitFont = text.font;
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnPointerEnter(PointerEventData pointerEventData) {

		text.font = enterFont;

	}

	public void OnPointerExit(PointerEventData pointerEventData) {

		text.font = exitFont;

	}

}
