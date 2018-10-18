using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCollision : MonoBehaviour {
	
	public bool isProtected = false;
	public bool isInvincible = false;
    public bool isSpeedObjectActivated = false;
    public bool isScreenBlockActivated = false;
    public float objectEffectiveTime = 10f;

    public Shader invincibleShader;

	public GameObject dinosaur;
    public RawImage ink;

	private Shader defaultShader;

	private LifeManager lifeManager;
    private LaneProperties laneProperties;

    private Coroutine invincibleCoroutine;
    private Coroutine speedObjectCoroutine;
    private Coroutine screenBlockCoroutine;

    public float speedDelta = 5f;
    public float fadeTime = 1f;
    public float lastFadeTime = -1;

	// Use this for initialization
	void Start () {

        lifeManager = GameObject.FindWithTag("GameController")
                      .GetComponent<LifeManager>();
        laneProperties = GameObject.FindWithTag("GameController")
                                   .GetComponent<LaneProperties>();

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
            } else if (collider.name.StartsWith("SpeedDown")) {
                if (!isSpeedObjectActivated) {
                    speedObjectCoroutine = StartCoroutine(SpeedDown());
                } else {
                    StopCoroutine(speedObjectCoroutine);
                    speedObjectCoroutine = StartCoroutine(SpeedDown());
                }
            } else if (collider.name.StartsWith("SpeedUp")) {
                if (!isSpeedObjectActivated) {
                    speedObjectCoroutine = StartCoroutine(SpeedUp());
                } else {
                    StopCoroutine(speedObjectCoroutine);
                    speedObjectCoroutine = StartCoroutine(SpeedUp());
                }
            } else if (collider.name.StartsWith("InkJar")) {
                if (!isScreenBlockActivated) {
                    screenBlockCoroutine = StartCoroutine(ScreenBlock());
                } else {
                    StopCoroutine(screenBlockCoroutine);
                    screenBlockCoroutine = StartCoroutine(ScreenBlock());
                }
            } else if (collider.name.StartsWith("Heart")) {
                lifeManager.gainLive();
            }
            return;
        }

        if (isProtected || isInvincible) {
			return;
		}

		// Obstacle collision
		if (collider.tag == "Obstacle") {
            Debug.Log("Obstacle: " + collider.name);
			lifeManager.loseLive();	
		}

	}

	IEnumerator InvincibleMode () {

		isInvincible = true;

		MeshRenderer renderer = dinosaur.GetComponent<MeshRenderer>();

		renderer.material.shader = invincibleShader;

		yield return new WaitForSeconds(objectEffectiveTime);

		isInvincible = false;
		renderer.material.shader = defaultShader;

	}

    IEnumerator SpeedUp() {
        isSpeedObjectActivated = true;

        laneProperties.effectSpeed = speedDelta;

        yield return new WaitForSeconds(objectEffectiveTime);

        isSpeedObjectActivated = false;
        laneProperties.effectSpeed = 0;
    }

    IEnumerator SpeedDown() {
        isSpeedObjectActivated = true;

        laneProperties.effectSpeed = -speedDelta;

        yield return new WaitForSeconds(objectEffectiveTime);

        isSpeedObjectActivated = false;
        laneProperties.effectSpeed = 0;
    }

    IEnumerator ScreenBlock() {
        // Show screen block
        if (!isScreenBlockActivated) {
            lastFadeTime = Time.time;
            while (Time.time < lastFadeTime + fadeTime) {
                ink.color = new Color32(0, 0, 0, (byte) Mathf.Lerp(0, 180, (Time.time - lastFadeTime) / fadeTime));
                yield return null;
            }
        }
                    
        isScreenBlockActivated = true;

        ink.color = new Color32(0, 0, 0, 180);

        yield return new WaitForSeconds(objectEffectiveTime);

        // hide screen block
        lastFadeTime = Time.time;
        while (Time.time < lastFadeTime + fadeTime) {
            ink.color = new Color32(0, 0, 0, (byte) Mathf.Lerp(180, 0, (Time.time - lastFadeTime) / fadeTime));
            yield return null;
        }
        isScreenBlockActivated = false;
        ink.color = new Color32(0, 0, 0, 0);
    }
}
