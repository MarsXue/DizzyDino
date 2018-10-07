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

        Debug.Log(gameObject.name + " -- " + renderers.Length);
    }
	
	// Update is called once per frame
	void Update () {

        // Move the object
        float speed = laneProperties.GetSpeed();
        float time = Time.fixedDeltaTime;
        Vector3 v = gameObject.transform.position;
        v.x += time * speed;
        gameObject.transform.position = v;


        //if (renderers.Length > 0) {
        //    bool isVisible = false;
        //    foreach (Renderer r in renderers) {
        //        isVisible = isVisible || r.isVisible;
        //    }
        //    if (!isVisible) {
        //        Destroy(gameObject);
        //    }
        //}
    }

    public void OnBecameInvisible() {
        Destroy(gameObject);
    }
}
