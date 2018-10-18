using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemOnCollide : MonoBehaviour {

    public AudioClip clip;
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

		if (gameObject.name.StartsWith("SpeedDown")) {
			Debug.Log("Speed Down collide" + collider.tag);
		}

		if (collider.tag == "Player") {

            if (clip != null)
                AudioSource.PlayClipAtPoint(clip, collider.transform.position, 1.0f);

			if (gameObject.tag == "Item") {
				Destroy(gameObject);

			} else {

				if (playerCollision.isInvincible) {
					
					Destroy(gameObject);
				}

			}

		}

	}

}
