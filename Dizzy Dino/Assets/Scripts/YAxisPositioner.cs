using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YAxisPositioner : MonoBehaviour {

	public float FixedY;

	// Use this for initialization
	void Start () {

		Vector3 p = gameObject.transform.position;

		p.y = FixedY;

		gameObject.transform.position = p;
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
