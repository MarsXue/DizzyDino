using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemOnCollide : MonoBehaviour {

	private PlayerCollision playerCollision;

	// Use this for initialization
	void Start () {

		playerCollision = GameObject.FindWithTag("Player")
						 .GetComponent<PlayerCollision>();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter (Collider collider) {

		if (collider.tag == "Item") {

			Destroy(gameObject);

		} else {

			if (playerCollision.isInvincible) {
				
				Destroy(gameObject);
			}

		}
		

	}

}
