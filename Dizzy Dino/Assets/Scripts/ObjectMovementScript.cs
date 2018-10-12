using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMovementScript : MonoBehaviour {

    private LaneProperties laneProperties;
    private Renderer[] renderers;

	// Use this for initialization
	void Start () {
        laneProperties =
            GameObject.FindWithTag("GameController")
                      .GetComponent<LaneProperties>();
        renderers = gameObject.GetComponentsInChildren<Renderer>();
    }
	
	// Update is called once per frame
	void Update () {

        // Move the object
        float speed = laneProperties.GetSpeed();
        float time = Time.smoothDeltaTime;
        Vector3 v = gameObject.transform.position;
        v.x += time * speed;
        gameObject.transform.position = v;
    }

    public void OnBecameInvisible() {
        Destroy(gameObject);
    }
}
