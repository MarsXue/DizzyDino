using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour {
	
	public bool isProtected = false;
	public bool isInvincible = false;
	public float invincibleTime = 10f;

	public Shader invincibleShader;

	public GameObject dinosaur;

	private Shader defaultShader;

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

		if (isProtected || isInvincible) {
			return;
		}

		// Obstacle collision
		if (collider.tag == "Obstacle") {
			Debug.Log("Obstacle: " + collider.name);
			lifeManager.loseLive();	
		}

		// Item collision
		if (collider.tag == "Item") {
			Debug.Log("Item: " + collider.name);
			
			if (collider.name.StartsWith("SuperStar")) {

				if (!isInvincible) {
					StartCoroutine("InvincibleMode");
				} else {
					StopCoroutine("InvincibleMode");
					StartCoroutine("InvincibleMode");
				}
				
			}
		}

	}

	IEnumerator InvincibleMode () {

		isInvincible = true;

		MeshRenderer renderer = dinosaur.GetComponent<MeshRenderer>();
		Shader defaultShader = renderer.material.shader;


		renderer.material.shader = invincibleShader;

		yield return new WaitForSeconds(invincibleTime);

		isInvincible = false;
		renderer.material.shader = defaultShader;

	}

}
