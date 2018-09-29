using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float laneWidth = 3f;
    private float z = 0.0f;

	// Use this for initialization
	void Start () {
        z = gameObject.transform.localPosition.z;

    }
	
	// Update is called once per frame
	void Update () {

        Vector3 pos = gameObject.transform.localPosition;
        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            pos.z = Mathf.Max(z - laneWidth, pos.z - laneWidth);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow)) {
            pos.z = Mathf.Min(z + laneWidth, pos.z + laneWidth);
        }
        gameObject.transform.localPosition = pos;

    }
}
