using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YAxisPositioner : MonoBehaviour {

	public Vector3 Position;

	public Vector3 Rotation;

	// Use this for initialization
	void Start () {

		Vector3 pos = gameObject.transform.position;
		pos.y = Position.y;
		gameObject.transform.position = pos;

        gameObject.transform.eulerAngles = Rotation;
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
