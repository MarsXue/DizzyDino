using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LiveManager : MonoBehaviour {
    public int defaultLives = 2;
    public Text lifeIndicator;

    public int lives { get; private set; }
    private const string LIFE_PREFIX = "♥×";

    // Use this for initialization
    void Start () {
        lives = defaultLives;
        UpdateIndicator();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void UpdateIndicator() {
        lifeIndicator.text = LIFE_PREFIX + lives;
    }

    void gainLive() {
        lives += 1;
        UpdateIndicator();
    }

    void loseLive() {
        lives -= 1;
        if (lives < 0) {
            GameOver();
        }
        UpdateIndicator();
    }

    void GameOver() {
        // TODO: Game over.
    }
}
