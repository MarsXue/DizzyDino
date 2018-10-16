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

    private Coroutine invincibleCoroutine;

    private AudioSource audioSource;
    public AudioClip hitSound;

	// Use this for initialization
	void Start () {
        audioSource = gameObject.GetComponent<AudioSource>();

        lifeManager = GameObject.FindWithTag("GameController")
                      .GetComponent<LifeManager>();

        defaultShader = dinosaur.GetComponent<Renderer>().material.shader;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter (Collider collider) {

        // Item collision
        if (collider.tag == "Item") {
            Debug.Log("Item: " + collider.name);
            if (collider.name.StartsWith("SuperStar")) {
                if (!isInvincible) {
                    invincibleCoroutine = StartCoroutine(InvincibleMode());
                } else {
                    StopCoroutine(invincibleCoroutine);
                    invincibleCoroutine = StartCoroutine(InvincibleMode());
                }
            }
            return;
        }

        if (isProtected || isInvincible) {
			return;
		}

		// Obstacle collision
		if (collider.tag == "Obstacle") {
            audioSource.clip = hitSound;
            audioSource.Play();
            Debug.Log("Obstacle: " + collider.name);
			lifeManager.loseLive();	
		}

	}

	IEnumerator InvincibleMode () {

		isInvincible = true;

		MeshRenderer renderer = dinosaur.GetComponent<MeshRenderer>();

		renderer.material.shader = invincibleShader;

		yield return new WaitForSeconds(invincibleTime);

		isInvincible = false;
		renderer.material.shader = defaultShader;

	}

}
