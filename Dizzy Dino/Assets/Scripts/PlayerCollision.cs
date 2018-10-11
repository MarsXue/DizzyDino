using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour {

	private LifeManager lifeManager;

	// Use this for initialization
	void Start () {
		
		lifeManager = GameObject.FindWithTag("GameController")
                      .GetComponent<LifeManager>();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter (Collider collider) {
		// if (collider.name.IndexOf("Ground") == -1) {
		// 	Debug.Log(collider.name);
		// }

		// Obstacle collision
		if (collider.tag == "Obstacle") {
			Debug.Log("Obstacle: " + collider.name);

			lifeManager.loseLive();			
		}

		// Item collision
		if (collider.tag == "Item") {
			Debug.Log("Item: " + collider.name);
		}

	}

}
