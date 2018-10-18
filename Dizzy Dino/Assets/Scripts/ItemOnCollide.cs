using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemOnCollide : MonoBehaviour {

	private AudioSource audioSource;
	private PlayerCollision playerCollision;

	// Use this for initialization
	void Start () {

		playerCollision = GameObject.FindWithTag("Player")
						 .GetComponent<PlayerCollision>();
		
		audioSource = gameObject.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter (Collider collider) {

		audioSource.Play();

		if (gameObject.tag == "Item") {

			Destroy(gameObject);

		} else {

			if (playerCollision.isInvincible) {
				
				Destroy(gameObject);
			}

		}
		

	}

}
