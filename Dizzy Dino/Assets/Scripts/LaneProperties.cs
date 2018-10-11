using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LaneProperties : MonoBehaviour {

	public static float ACCELERATION = 0.05f;
	public float maxSpeed = 25.0f;
    public float speed;
    public float effectSpeed;
	public float laneWidth;
	public bool accerlerate = true;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if (accerlerate) {
			speed += ACCELERATION * Time.deltaTime;
		}

	}

    public float GetSpeed() {
        return speed + effectSpeed;
    }

}
