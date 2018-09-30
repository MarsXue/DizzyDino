using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float laneWidth = 3f;
    public float force = 450f;
    public float jumpThreshold = 0.5f;
    private float z = 0.0f;
    private Rigidbody r;
    

    // Use this for initialization
    void Start () {
        z = gameObject.transform.localPosition.z;
        r = gameObject.GetComponent<Rigidbody>();
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

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)) {
            if (gameObject.transform.position.y < jumpThreshold) {
                r.AddForce(force * Vector3.up);
            }
        }
    }
}
