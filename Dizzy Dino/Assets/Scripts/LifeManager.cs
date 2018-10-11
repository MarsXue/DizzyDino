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

    public Text endText;
    public Image endImage;

    public GameObject dinosaur;

    private Spawner spawner;
    private ScoreManager scoreManager;
    private LaneProperties laneProperties;

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
    }

    void GameOver() {
        // TODO: Game over.
        Debug.Log("Game Over");
        
        endText.enabled = true;
        endImage.enabled = true;

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
    void RestartGame() {

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
