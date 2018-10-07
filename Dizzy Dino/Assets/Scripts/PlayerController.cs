﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private float laneWidth;
    public float force = 450f;
    public float jumpThreshold = 0.5f;
    public float rotationLimit = 45f;

    public ParticleSystem particleSystem;

    private LaneProperties laneProperties;

    private float z = 0.0f;
    private Rigidbody r;
    

    // Use this for initialization
    void Start () {
        laneProperties =
            GameObject.FindWithTag("GameController")
                      .GetComponent<LaneProperties>();
        z = gameObject.transform.localPosition.z;
        r = gameObject.GetComponent<Rigidbody>();

        laneWidth = laneProperties.laneWidth;
    }
	
	// Update is called once per frame
	void Update () {
      
        Vector3 pos = gameObject.transform.localPosition;
        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            pos.z = Mathf.Max(z - laneWidth, pos.z - laneWidth);
            r.angularVelocity = Vector3.zero;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow)) {
            pos.z = Mathf.Min(z + laneWidth, pos.z + laneWidth);
            r.angularVelocity = Vector3.zero;
        }
        gameObject.transform.localPosition = pos;

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)) {
            if (gameObject.transform.position.y < jumpThreshold) {
                r.AddForce(force * Vector3.up);
            }
        }

        if (gameObject.transform.position.y > jumpThreshold) {
            particleSystem.Stop();
            r.angularVelocity = Vector3.zero;
        } else {
            particleSystem.Play();
        }
    }
}
