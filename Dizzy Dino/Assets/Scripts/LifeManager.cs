using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LifeManager : MonoBehaviour {
    public int defaultLives = 2;
    public Text lifeIndicator;

    public int lives { get; private set; }
    private const string LIFE_PREFIX = "♥×";

    public GameObject endScreen;
    public GameObject hiScoreScreen;

    public GameObject dinosaur;

    private Spawner spawner;
    private ScoreManager scoreManager;
    private LaneProperties laneProperties;
    private PlayerCollision playerCollision;
    private TutorialTextManager tutorialTextManager;

    private bool isVisible = true;


    private float shakeStartTime = -1f;
    private float shakeTime = 0.5f;
    private RectTransform rectTrans;

    // Use this for initialization
    void Start () {

        lives = defaultLives;
        UpdateIndicator();

        spawner = GameObject.FindWithTag("Spawner")
                  .GetComponent<Spawner>();
        
        scoreManager = GameObject.FindWithTag("GameController")
                       .GetComponent<ScoreManager>();
        
        laneProperties = GameObject.FindWithTag("GameController")
                        .GetComponent<LaneProperties>();

        tutorialTextManager = GameObject.FindWithTag("GameController")
                                   .GetComponent<TutorialTextManager>();

        playerCollision = dinosaur.GetComponent<PlayerCollision>();

        rectTrans = lifeIndicator.GetComponent<RectTransform>();

	}
	
	// Update is called once per frame
	void Update () {
        Vector2 pos = rectTrans.anchoredPosition;
        if (Time.time < shakeStartTime + shakeTime) {
            pos.x = Random.Range(-10, 10);
        } else if (shakeStartTime > 0) {
            pos.x = 0;
            shakeStartTime = -1f;
        }
        rectTrans.anchoredPosition = pos;
	}

    void UpdateIndicator() {
        if (lives == 10) {
            lifeIndicator.text = LIFE_PREFIX + lives + "(MAX)";
        } else {
            lifeIndicator.text = LIFE_PREFIX + lives;
        }
    }

    public void gainLive() {
        if (lives < 10) {
            lives += 1;
            UpdateIndicator();
        } else {
            shakeStartTime = Time.time;
        }
    }

    public void loseLive() {
        lives -= 1;
        if (lives <= 0) {
            GameOver();
        }
        UpdateIndicator();

        StartCoroutine(DoBlinks(1f, 0.2f));
    }

    IEnumerator DoBlinks(float duration, float blinkTime) {

		playerCollision.isProtected = true;

        while (duration > 0f) {
            
            duration -= blinkTime;
      
            //toggle renderer
            isVisible = !isVisible;
            setVisibility(dinosaur, isVisible);
      
            //wait for a bit
            yield return new WaitForSeconds(blinkTime);
        }
  
        //make sure renderer is enabled when we exit
        isVisible = true;
        setVisibility(dinosaur, isVisible);

        playerCollision.isProtected = false;

    }

    void setVisibility(GameObject g, bool v) {
        MeshRenderer mr = g.GetComponent<MeshRenderer>();

        if (mr != null) mr.enabled = v;

        foreach (Transform child in g.transform) {
            setVisibility(child.gameObject, v);
        }
    }

    void GameOver() {
        Debug.Log("Game Over");

        // Stop all kinds of stuff
        spawner.stop = true;
        scoreManager.stop = true;
        tutorialTextManager.stop = true;
        
        // Stop moving the landscape
        laneProperties.speed = 0;
        laneProperties.effectSpeed = 0;
        laneProperties.accerlerate = false;

        // Stop moving the dinosuar
        dinosaur.GetComponent<Rigidbody>().isKinematic = false;
        ParticleSystem.EmissionModule e = dinosaur.GetComponentInChildren<ParticleSystem>().emission;
        e.enabled = false;
        dinosaur.GetComponentInChildren<PlayerController>().stop = true;

        // Check if new high score is achieved
        if (ScoreManager.score * 10 > RankingManager.GetWorstScore()) {
            hiScoreScreen.SetActive(true);
            gameObject.GetComponent<NewHiScore>().Focus();
        } else {
            endScreen.SetActive(true);
        }
    }

}
