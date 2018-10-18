using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    public static float score = 0f;
    public Text highScore;
    public Text scoreText;

    private bool isBlinking = false;
    public bool stop = false;

    private float time = 0.0f;

    private AudioSource scoreAudioSource;

    // Use this for initialization
    void Start () {
        score = 0f;
        scoreAudioSource = gameObject.GetComponent<AudioSource>();
        highScore.text = "HI " + PlayerPrefs.GetInt("score0").ToString().PadLeft(5, '0');
    }
	
    // Update is called once per frame
    void Update () {
        
        // Stop counting the score
        if (stop) {
            return;
        }

        score += Time.deltaTime;
        int s = (int) (score * 10);

        if (isBlinking) {
            
            time += Time.deltaTime;

            if (time > 2.0f) {
                time = 0.0f;
                isBlinking = false;

                StopBlinking();
            }

        } else {
            if (s % 100 == 0 && s != 0) {
                isBlinking = true;
                StartBlinking();
            }

            scoreText.text = s.ToString().PadLeft(5, '0');

        }

    }

    IEnumerator Blink() {

		while (!stop) {

			switch(scoreText.color.a.ToString()) {
				case "0":
					scoreText.color = new Color(scoreText.color.r, 
										        scoreText.color.g, 
										        scoreText.color.b, 1);
					yield return new WaitForSeconds(0.25f);
					break;
				case "1":
					scoreText.color = new Color(scoreText.color.r, 
										        scoreText.color.g, 
										        scoreText.color.b, 0);
					yield return new WaitForSeconds(0.25f);
					break;
			}

		}

	}
	
	public void StartBlinking() {
        scoreAudioSource.Play();

		StopCoroutine("Blink");
		StartCoroutine("Blink");
	
	}

	public void StopBlinking() {

		StopCoroutine("Blink");

	}

}
