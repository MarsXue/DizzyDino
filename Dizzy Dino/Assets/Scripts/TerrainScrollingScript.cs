using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainScrollingScript : MonoBehaviour {

    private LaneProperties speedController;
    private float startingX = 0.0f;
    private const float size = 10.0f;

	// Use this for initialization
	void Start () {
        speedController =
            GameObject.FindWithTag("GameController")
                      .GetComponent<LaneProperties>();
        startingX = gameObject.transform.position.x;
    }
	
	// Update is called once per frame
	void Update () {
        float speed = speedController.speed;
        float time = Time.fixedDeltaTime;
        Vector3 v = gameObject.transform.position;
        v.x = startingX + (v.x + time * speed) % size;
        gameObject.transform.position = v;
    }
}
