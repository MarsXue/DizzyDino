using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaneProperties : MonoBehaviour {

    public float speed;
    public float effectSpeed;
	public float laneWidth;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public float GetSpeed() {
        return speed + effectSpeed;
    }
}
