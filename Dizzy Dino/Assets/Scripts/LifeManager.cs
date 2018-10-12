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

    public GameObject dinosaur;

    private Spawner spawner;
    private ScoreManager scoreManager;
    private LaneProperties laneProperties;
    private PlayerCollision playerCollision;

    private bool isVisible = true;

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

        playerCollision = dinosaur.GetComponent<PlayerCollision>();


	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void UpdateIndicator() {
        lifeIndicator.text = LIFE_PREFIX + lives;
    }

    public void gainLive() {
        lives += 1;
        UpdateIndicator();
    }

    public void loseLive() {
        lives -= 1;
        if (lives <= 0) {
            GameOver();
        }
        UpdateIndicator();

        StartCoroutine(DoBlinks(3f, 0.2f));
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

        endScreen.SetActive(true);

        // Stop generating the assets
        spawner.stop = true;

        // Stop adding the score
        scoreManager.stop = true;
        
        // Stop moving the landscape
        laneProperties.speed = 0;
        laneProperties.effectSpeed = 0;
        laneProperties.accerlerate = false;

        // Stop moving the dinosuar
        dinosaur.GetComponent<Rigidbody>().isKinematic = false;
        dinosaur.GetComponentInChildren<ParticleSystem>().enableEmission = false;
        dinosaur.GetComponentInChildren<PlayerController>().stop = true;

    }

    // Restart the game
    public void RestartGame() {

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ReturnToMenu() {
        SceneManager.LoadScene("MainMenu");
    }

}
