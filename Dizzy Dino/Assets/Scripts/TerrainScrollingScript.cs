using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainScrollingScript : MonoBehaviour {

    public float speed = 0.0f;
    private float startingX = 0.0f;

    private const float size = 10.0f;

	// Use this for initialization
	void Start () {
        startingX = gameObject.transform.position.x;
    }
	
	// Update is called once per frame
	void Update () {
        float time = Time.fixedDeltaTime;
        Vector3 v = gameObject.transform.position;
        v.x = startingX + (v.x + time * speed) % size;
        gameObject.transform.position = v;
    }
}
