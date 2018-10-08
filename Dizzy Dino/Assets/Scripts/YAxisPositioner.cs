using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YAxisPositioner : MonoBehaviour {

	public float PositionY;

	public float RotationY;

	// Use this for initialization
	void Start () {

		Vector3 position = gameObject.transform.position;

		position.y = PositionY;

		gameObject.transform.position = position;

		Vector3 eulerAngles = gameObject.transform.eulerAngles;

		eulerAngles.y = RotationY;

		gameObject.transform.eulerAngles = eulerAngles;
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
